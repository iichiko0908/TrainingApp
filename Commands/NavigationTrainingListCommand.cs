using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.ViewModels;
using TrainingApp.Views;

namespace TrainingApp.Commands
{
    internal class NavigationTrainingListCommand : ICommand
    {
        /// <summary>
        /// 日付
        /// </summary>
        private TrainingDateTimeSelectViewModel TraningDateTimeSelectViewModel { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="viewModel"></param>
        public NavigationTrainingListCommand(TrainingDateTimeSelectViewModel viewModel) 
        {
            this.TraningDateTimeSelectViewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 判定
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList.Add("DateTime", this.TraningDateTimeSelectViewModel.TraningDateTime);
            Shell.Current.GoToAsync($"{nameof(TrainingRecordListView)}", parameterList);
        }
    }
}
