using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Models;
using TrainingApp.Models.DB;
using TrainingApp.Views;

namespace TrainingApp.ViewModels
{
    internal class TrainingRecordAddViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        /// <summary>
        /// 
        /// </summary>
        private TrainingRecordAddModel TrainingRecordAddModel { get; set; }
        /// <summary>
        /// トレーニング記録リスト
        /// </summary>
        public ObservableCollection<TrainingRecordList> TrainingRecordListCollection {  get; set; }
        /// <summary>
        /// トレーニングマスタ
        /// </summary>
        private TrainingMaster _trainingMaster { get; set; }
        public TrainingMaster TrainingMaster {
            get
            {
                return _trainingMaster;
            }
            set
            {
                _trainingMaster = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 削除したトレーニング記録一覧
        /// </summary>
        public List<TrainingRecordList> RemoveTrainingRecordLists { get ; set; }
        /// <summary>
        /// 選択した日付
        /// </summary>
        public DateTime TrainingDateTimeSelected { get; set; }
        /// <summary>
        /// 行追加コマンド
        /// </summary>
        public ICommand RowAddTrainingRecordListCommand
        {
            get
            {
                return new Command(
                    execute: (object? parameter) =>
                    {
                        // 空白行を追加する。
                        this.TrainingRecordListCollection.Add(new TrainingRecordList() { SetNumber = this.TrainingRecordListCollection.Count + 1 });
                    },
                    canExecute: (object? parameter) =>
                    {
                        return true;
                    });
            }
        }
        /// <summary>
        /// 行削除
        /// </summary>
        public ICommand RowDeleteTrainingRecordListCommand
        {
            get
            {
                return new Command(
                    (object? parameter) =>
                    {
                        // 選択した行を削除する
                        TrainingRecordList trainingRecordList = (TrainingRecordList)parameter;
                        this.TrainingRecordListCollection.Remove(trainingRecordList);

                        // 退避する
                        this.RemoveTrainingRecordLists.Add(trainingRecordList);
                    });
            }
        }

        public ICommand UpdateEntryWeightTrainingRecordList
        {
            get
            {
                return new Command(

                    (object? parameter) =>
                    {

                    });
            }
        }

        /// <summary>
        /// 戻るボタン
        /// </summary>
        public ICommand NavigationTrainingListViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    // トレーニング記録登録・更新
                    InsertUpdateTrainingRecordList();

                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("DateTime", this.TrainingDateTimeSelected);
                    // トレーニング一覧画面に遷移
                    Shell.Current.GoToAsync(nameof(TrainingRecordListView), parameters);
                });
            }
        }

        /// <summary>
        /// 登録ボタンコマンド
        /// </summary>
        public ICommand InsertUpdateTrainingRecordListCommand
        {
            get
            {
                return new Command(
                    (object? parameter) =>
                    {
                        // トレーニング記録登録・更新
                        InsertUpdateTrainingRecordList();
                        Application.Current.MainPage.DisplayAlert("トレーニング記録", "登録完了しました", "閉じる");
                    });
            }
        }

        /// <summary>
        /// 変更
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// プロパティの変更通知を起動する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TrainingRecordAddViewModel()
        {
            this.TrainingRecordAddModel = new TrainingRecordAddModel();
            this.TrainingRecordListCollection = new ObservableCollection<TrainingRecordList>();

            this.RemoveTrainingRecordLists = new List<TrainingRecordList>();
        }


        /// <summary>
        /// 引数受け取り
        /// </summary>
        /// <param name="query"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            {
                // 引数取得
                object trainingMasterId = query["TrainingMasterId"];
                this.TrainingDateTimeSelected = DateTime.Parse(query["DateTime"].ToString());

                using (TrainingDBContext context = new TrainingDBContext())
                {
                    // トレーニングマスタ
                    this.TrainingMaster = context.Connection.Table<TrainingMaster>().Where(t => t.Id == (int)trainingMasterId).First();
                    // トレーニング記録リスト
                    IEnumerable<TrainingRecordList> trainingRecordList =
                        context.Connection.Table<TrainingRecordList>().Where(
                            t => t.TrainingMasterId == (int)trainingMasterId &&
                            t.Created == this.TrainingDateTimeSelected);

                    // 取得できなかった場合、新規
                    if (!trainingRecordList.Any())
                    {
                        // 次のセット数を設定する
                        this.TrainingRecordListCollection.Add(new TrainingRecordList() { SetNumber = this.TrainingRecordListCollection.Count + 1 });
                    }
                    // 取得できた場合
                    else
                    {
                        foreach (TrainingRecordList recored in trainingRecordList)
                        {
                            TrainingRecordListCollection.Add(recored);
                        }
                    }
                }
                
            }
            catch(Exception ex) 
            {
                throw;
            }
        }
        /// <summary>
        /// トレーニング記録登録・更新
        /// </summary>
        private void InsertUpdateTrainingRecordList()
        {
            // ***********************
            // DBに登録する
            // ***********************
            using (TrainingDBContext context = new TrainingDBContext())
            {
                // トランザクション
                context.BeginTrainsaction();
                foreach (TrainingRecordList trainingRecored in TrainingRecordListCollection)
                {
                    // 入力している場合
                    if (0 < trainingRecored.Weight && 0 < trainingRecored.NumberOfTimes)
                    {
                        // IDが0の場合、新規登録
                        if (trainingRecored.Id == 0)
                        {
                            // トレーニングマスタ
                            trainingRecored.TrainingMasterId = this.TrainingMaster.Id;
                            // 現在日
                            trainingRecored.Created = DateTime.Parse(DateTime.Now.ToShortDateString());
                            // 登録
                            trainingRecored.Insert(context);
                        }
                        else
                        {
                            // 更新
                            trainingRecored.Update(context);
                        }
                    }
                }

                // 削除したトレーニング記録一覧をDBから削除する
                foreach (TrainingRecordList trainingRecord in this.RemoveTrainingRecordLists)
                {
                    if (trainingRecord.Id != 0)
                    {
                        // 削除
                        trainingRecord.Delete(context);
                    }
                }
                // コミット
                context.Commit();
            }
        }
    }
}
