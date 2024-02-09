using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Commands;
using TrainingApp.Models;
using TrainingApp.Models.DB;

namespace TrainingApp.ViewModels
{
    internal class SampleDataBaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 画面項目クラス
        /// </summary>
        private SampleDataBaseModel SampleDataBaseModel { get; set; }
        /// <summary>
        /// 画面項目（種目一覧）
        /// </summary>
        private ObservableCollection<SampleEventMaster> ColSampleEventMaster { get; set; }
        /// <summary>
        /// 変更イベント
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #region コマンド
        /// <summary>
        /// コマンド（種類マスタ登録）
        /// </summary>
        public ICommand AddEventMasterCommand { get; set; }
        /// <summary>
        /// コマンド（種類マスタ削除）
        /// </summary>
        public ICommand RemoveEventMasterCommand { get; set; }
        #endregion

        #region 画面項目プロパティ
        public string WeekName
        {
            get { return this.SampleDataBaseModel.WeekName; }
            set
            {
                if (this.SampleDataBaseModel.WeekName != value)
                {
                    this.SampleDataBaseModel.WeekName = value;
                    NotifyPropertyChanged();
                    // ボタン実行可否
                    ((AddEventmasterCommand)this.AddEventMasterCommand).RiseCanExecuteChanged();
                }
            }
        }
        public string EventName
        {
            get { return this.SampleDataBaseModel.EventName; }
            set
            {
                if (this.SampleDataBaseModel.EventName != value)
                {
                    this.SampleDataBaseModel.EventName = value;
                    NotifyPropertyChanged();
                    // ボタン実行可否
                    ((AddEventmasterCommand)this.AddEventMasterCommand).RiseCanExecuteChanged();
                }
            }
        }
        public ObservableCollection<SampleEventMaster> EventMasterList
        {
            get { return this.ColSampleEventMaster; }
            set
            {
                this.ColSampleEventMaster = value;
                // 変更イベント
                NotifyPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// ViewModelコンストラクタ
        /// </summary>
        public SampleDataBaseViewModel()
        {
            // 画面項目初期化
            this.SampleDataBaseModel = new SampleDataBaseModel();
            // コマンド登録
            this.AddEventMasterCommand = new AddEventmasterCommand(this);
            this.RemoveEventMasterCommand = new RemoveEventMasterCommand(this);

            // 一覧取得
            SetEventMasterList();
        }

        /// <summary>
        /// プロパティの変更通知を起動する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 処理

        private void SetEventMasterList()
        {
            using (SampleEventMasterDbContext context = new SampleEventMasterDbContext())
            {
                // 種目一覧設定
                this.EventMasterList =
                    new ObservableCollection<SampleEventMaster>(context.Connection.Table<SampleEventMaster>().ToList());
            }
        }
        #endregion
    }
}
