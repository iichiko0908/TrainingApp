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
    internal class RemoveEventMasterCommand : ICommand
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
        public RemoveEventMasterCommand(SampleDataBaseViewModel viewModel)
        {
            this.SampleDataBaseViewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            //EventMaster selected = (EventMaster)((SwipeItem)sender).CommandParameter;
            SampleEventMaster deleteSampleEventMaster = (SampleEventMaster)parameter;

            using (SampleEventMasterDbContext context = new SampleEventMasterDbContext())
            {
                // 種目マスタ削除
                deleteSampleEventMaster.Delete(context);

                // 種目一覧設定
                this.SampleDataBaseViewModel.EventMasterList =
                    new ObservableCollection<SampleEventMaster>(context.Connection.Table<SampleEventMaster>().ToList());
            }
        }
    }
}
