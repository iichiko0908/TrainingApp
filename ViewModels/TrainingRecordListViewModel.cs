using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    internal class TrainingRecordListViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        /// <summary>
        /// トレーニング記録一覧のグルーピングリスト
        /// </summary>
        public List<TrainingRecordListGroup> TrainingRecordListGroup
        {
            get; set;
        }
        /// <summary>
        /// 種目追加ボタンイベント
        /// </summary>
        public ICommand NavigationTrainingMasterCommand { get; set; }
        /// <summary>
        /// 戻るイベント
        /// </summary>
        public ICommand NavigationTrainingDateTimeSelectViewCommand
        {
            get
            {
                return new Command(
                    execute: (object? parameter) =>
                    {
                        Shell.Current.GoToAsync("//MainManu/TrainingDateTimeSelectView");
                    });
            }
        }

        public ICommand NavigationTrainingRecordAddViewCommand
        {
            get
            {
                return new Command(
                    execute: (object? parameter) =>
                    {
                        Dictionary<string, object> parameters = new Dictionary<string, object>();
                        parameters.Add("TrainingMasterId", parameter);
                        parameters.Add("DateTime", this.TrainingDateTimeSelected); 
                        // トレーニング記録登録へ遷移
                        Shell.Current.GoToAsync(nameof(TrainingRecordAddView), parameters);
                    });
            }
        }

        /// <summary>
        /// Model
        /// </summary>
        private TrainingRecordListModel TrainingListModel {  get; set; }
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime TrainingDateTimeSelected { get; set; }
        public string TrainingDateTimeSelectedString {  get; set; }

        /// <summary>
        /// 種目一覧
        /// </summary>
        public ObservableCollection<string> TrainingTypeList {  get; set; } 

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TrainingRecordListViewModel()
        {
            // 追加コマンド
            this.NavigationTrainingMasterCommand = new NavigationTrainingMasterCommand(this);
            this.TrainingRecordListGroup = new List<TrainingRecordListGroup>();
        }

        /// <summary>
        /// プロパティの変更通知を起動する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 引数受け取り
        /// </summary>
        /// <param name="query"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // 画面で選択した日付を設定する
            this.TrainingDateTimeSelected = DateTime.Parse(((DateTime)query["DateTime"]).ToShortDateString());
            this.TrainingDateTimeSelectedString = this.TrainingDateTimeSelected.ToString("yyyy/MM/dd");
            NotifyPropertyChanged("TrainingDateTimeSelected");
            // トレーニング記録リストの設定
            SetTrainingRecoredList();
        }

        /// <summary>
        /// トレーニング記録リストの設定
        /// </summary>
        private void SetTrainingRecoredList()
        {
            using (TrainingDBContext context = new TrainingDBContext())
            {
                // トレーニングマスタ
                IEnumerable<TrainingMaster> trainingMaster = context.Connection.Table<TrainingMaster>().OrderBy(t => t.Order) ;

                // 選択した日付のトレーニング記録を取得
                IEnumerable<TrainingRecordList> trainingRecordAllList = context.Connection.Table<TrainingRecordList>().Where(t => t.Created == this.TrainingDateTimeSelected);
                // トレーニングマスタIDでグルーピング（トレーニング種目ごと）
                IEnumerable<IEnumerable<TrainingRecordList>> trainingRecordPartList = trainingRecordAllList.GroupBy(t => t.TrainingMasterId);

                foreach (IEnumerable<TrainingRecordList> partList in trainingRecordPartList)
                {
                    // 種目名を取得
                    string eventName = trainingMaster.Where(t => t.Id == partList.First().TrainingMasterId).First().EventName;
                    // グルーピングしてリストに追加
                    this.TrainingRecordListGroup.Add(new TrainingRecordListGroup(eventName, partList.First().TrainingMasterId, partList.ToList()));
                }
            }
        }
    }
}
