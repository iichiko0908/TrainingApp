using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Models.DB;
using TrainingApp.ViewModels;

namespace TrainingApp.Commands
{
    internal class AddEventmasterCommand : ICommand
    {
        /// <summary>
        /// 画面項目
        /// </summary>
        private SampleDataBaseViewModel SampleDataBaseViewModel { get; set; }

        public event EventHandler? CanExecuteChanged;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="viewModel"></param>
        public AddEventmasterCommand(SampleDataBaseViewModel viewModel) 
        {
            this.SampleDataBaseViewModel = viewModel;
        }

        /// <summary>
        /// 実行判定の呼び出し
        /// </summary>
        public void RiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 実行判定
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(this.SampleDataBaseViewModel.WeekName) && !string.IsNullOrEmpty(this.SampleDataBaseViewModel.EventName);
        }

        /// <summary>
        /// 実行処理
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            using (SampleEventMasterDbContext context = new SampleEventMasterDbContext())
            {
                IEnumerable<SampleEventMaster> list =
                    context.Connection.Table<SampleEventMaster>().Where(t =>
                        t.WeekName == this.SampleDataBaseViewModel.WeekName && t.EventName == this.SampleDataBaseViewModel.EventName);
                // 取得できなかった場合
                if (0 == list.Count())
                {
                    // レコード作成
                    SampleEventMaster eventMaster = new SampleEventMaster()
                    {
                        WeekName = this.SampleDataBaseViewModel.WeekName,
                        EventName = this.SampleDataBaseViewModel.EventName,
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };
                    // レコード追加
                    eventMaster.Insert(context);
                    // 反映
                    this.SampleDataBaseViewModel.EventMasterList.Add(eventMaster);
                }
                else
                {
                    SampleEventMaster updateEventMaster = list.First<SampleEventMaster>();
                    updateEventMaster.Modified = DateTime.Now;
                    // レコード更新
                    updateEventMaster.Update(context);
                    // 種目一覧設定
                    this.SampleDataBaseViewModel.EventMasterList =
                        new ObservableCollection<SampleEventMaster>(context.Connection.Table<SampleEventMaster>().ToList());

                }
            }
        }

    }
}
