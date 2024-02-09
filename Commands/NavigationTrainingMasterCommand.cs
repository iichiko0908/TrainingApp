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
    class NavigationTrainingMasterCommand : ICommand
    {
        public TrainingRecordListViewModel TrainingListViewModel { get; set; }

        public event EventHandler? CanExecuteChanged;

        public NavigationTrainingMasterCommand(TrainingRecordListViewModel viewModel)
        {
            this.TrainingListViewModel = viewModel;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("DateTime", this.TrainingListViewModel.TrainingDateTimeSelected);
            // トレーニングマスタ一覧画面に遷移
            Shell.Current.GoToAsync(nameof(TrainingMasterView), parameters);
        }
    }
}
