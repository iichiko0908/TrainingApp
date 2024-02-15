using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Commands;
using TrainingApp.Models;
using TrainingApp.Models.DB;
using TrainingApp.Models.Group;
using TrainingApp.Views;

namespace TrainingApp.ViewModels
{
    class TrainingMasterViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        #region プロパティ

        public List<TrainingMasterGroup> TrainingMasterGroup { get; set; }

        /// <summary>
        /// 選択した日付
        /// </summary>
        public DateTime TrainingDateTimeSelected { get; set; }
        /// <summary>
        /// 登録コマンド
        /// </summary>
        public ICommand NavigationTrainingMasterAddCommand { get; set; }
        /// <summary>
        /// 編集コマンド
        /// </summary>
        public ICommand NavigationUpdateTrainingMasterCommand
        {
            get
            {
                return new Command(
                execute:(object? parameter) =>
                {
                    // 編集で選択したトレーニングマスタの行
                    TrainingMaster trainingMaster = (TrainingMaster)parameter;
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("TrainingMasterId", trainingMaster.Id);
                    parameters.Add("DateTime", this.TrainingDateTimeSelected);
                    // トレーニングマスタのＩＤを引数に登録画面に遷移
                    Shell.Current.GoToAsync(nameof(TrainingMasterAddView), parameters);
                },
                canExecute:(object? parammeter) =>
                {
                    return true;
                }
                );

            }
        }
        /// <summary>
        /// 削除コマンド
        /// </summary>
        public ICommand DeleteDBTrainingMasterCommand { get; set; }
        /// <summary>
        /// 戻るコマンド
        /// </summary>
        public ICommand NavigationTrainingListViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("DateTime", this.TrainingDateTimeSelected);
                    // トレーニング一覧画面に遷移
                    Shell.Current.GoToAsync(nameof(TrainingRecordListView), parameters);
                });
            }
        }

        public ICommand NavigationTrainingTrainingRecordCommand
        {
            get
            {
                return new Command(
                execute: (object? parameter) =>
                {
                    // 編集で選択したトレーニングマスタの行
                    TrainingMaster trainingMaster = (TrainingMaster)parameter;
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("TrainingMasterId", trainingMaster.Id);
                    parameters.Add("DateTime", this.TrainingDateTimeSelected);
                    // トレーニングマスタのＩＤを引数に登録画面に遷移
                    Shell.Current.GoToAsync(nameof(TrainingRecordAddView), parameters);
                },
                canExecute: (object? parameters) =>
                {
                    return true;
                });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        

        #endregion

        public TrainingMasterViewModel() 
        {
            this.NavigationTrainingMasterAddCommand = new NavigationTrainingMasterAddCommand(this);
            this.DeleteDBTrainingMasterCommand = new DeleteDBTrainingMasterCommand(this);
            this.TrainingMasterGroup = new List<TrainingMasterGroup>();

            // トレーニングマスタ一覧表示
            SetTrainingMaster();
        }

        /// <summary>
        /// プロパティの変更通知
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetTrainingCollectionView()
        {

        }
        /// <summary>
        /// トレーニングマスタ設定
        /// </summary>
        public void SetTrainingMaster()
        {
            using (TrainingDBContext context = new TrainingDBContext())
            {
                // 部位をグループ化して取得
                IEnumerable<IEnumerable<TrainingMaster>> trainingMasterGroup = context.Connection.Table<TrainingMaster>().GroupBy(t => t.Part);
                foreach (IEnumerable<TrainingMaster> trainingMasters in trainingMasterGroup) 
                {
                    // グループのリストに追加
                    this.TrainingMasterGroup.Add(new TrainingMasterGroup(trainingMasters.First().Part, trainingMasters.ToList()));
                }
            }
        }

        /// <summary>
        /// 引数受け取り
        /// </summary>
        /// <param name="query"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // 選択した日付を受け取り
            this.TrainingDateTimeSelected = DateTime.Parse(query["DateTime"].ToString());
        }

    }
}
