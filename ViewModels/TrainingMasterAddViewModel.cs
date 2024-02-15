using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Commands;
using TrainingApp.Models;
using TrainingApp.Models.DB;
using TrainingApp.Views;

namespace TrainingApp.ViewModels
{
    class TrainingMasterAddViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        /// <summary>
        /// 選択した日付
        /// </summary>
        public DateTime TrainingDateTimeSelected { get; set; }

        /// <summary>
        /// 登録コマンド
        /// </summary>
        public ICommand CreateDBTrainingMasterCommand { get; set; }
        /// <summary>
        /// 戻るコマンド
        /// </summary>
        public ICommand BackNavigationTrainingMasterCommand 
        { 
            get
            {
                return new Command(() =>
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("DateTime", this.TrainingDateTimeSelected);
                    // トレーニング一覧画面に遷移する
                    Shell.Current.GoToAsync(nameof(TrainingMasterView), parameters);
                });
            }
        }
        /// <summary>
        /// 画面項目
        /// </summary>
        private TrainingMasterAddModel TrainingMasterAddModel { get; set; }
        /// <summary>
        /// 部位
        /// </summary>
        public string Part
        {
            get
            {
                return this.TrainingMasterAddModel.Part;
            }
            set
            {
                this.TrainingMasterAddModel.Part = value;
                NotifyPropertyChanged();
                // 実行判定
                ((CreateDBTrainingMasterCommand)this.CreateDBTrainingMasterCommand).RiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 種目名
        /// </summary>
        public string EventName
        {
            get
            {
                return this.TrainingMasterAddModel.EventName;
            }
            set
            {
                this.TrainingMasterAddModel.EventName = value;
                NotifyPropertyChanged();
                // 実行判定
                ((CreateDBTrainingMasterCommand)this.CreateDBTrainingMasterCommand).RiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// 並び順
        /// </summary>
        public int? Order
        {
            get
            {
                return this.TrainingMasterAddModel.Order;
            }
            set
            {
                this.TrainingMasterAddModel.Order = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// トレーニングマスタID
        /// </summary>
        public int TrainingMasterId
        {
            get;set;
        }

        /// <summary>
        /// 変更通知
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        public TrainingMasterAddViewModel()
        {
            this.TrainingMasterAddModel = new TrainingMasterAddModel();
            this.CreateDBTrainingMasterCommand = new CreateDBTrainingMasterCommand(this);
        }

        /// <summary>
        /// プロパティの変更通知
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// クエリ受け取り
        /// </summary>
        /// <param name="query"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // 選択した日付を受け取り
            this.TrainingDateTimeSelected = DateTime.Parse(query["DateTime"].ToString());
            object trainingMasterId = null;
            if (query.TryGetValue("TrainingMasterId", out trainingMasterId))
            {
                using (TrainingDBContext context = new TrainingDBContext())
                {
                    // 編集用トレーニングマスタ取得
                    IEnumerable<TrainingMaster> trainingMaster = 
                        context.Connection.Table<TrainingMaster>().Where(t => t.Id == (int)trainingMasterId);

                    if (0 < trainingMaster.Count())
                    {
                        this.Part = trainingMaster.First<TrainingMaster>().Part;
                        this.EventName = trainingMaster.First<TrainingMaster>().EventName;
                        this.TrainingMasterId = trainingMaster.First<TrainingMaster>().Id;
                        this.Order = trainingMaster.First<TrainingMaster>().Order;
                    }
                }
            }
        }
    }
}
