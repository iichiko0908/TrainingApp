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
    class NavigationTrainingMasterAddCommand : ICommand
    {
        /// <summary>
        /// 画面項目
        /// </summary>
        private TrainingMasterViewModel TrainingMasterViewModel { get; set; }

        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="viewModel"></param>
        public NavigationTrainingMasterAddCommand(TrainingMasterViewModel viewModel)
        {
            this.TrainingMasterViewModel = viewModel;
        }
        /// <summary>
        /// 判定の呼び出し
        /// </summary>
        public void RiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
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
        /// <exception cref="NotImplementedException"></exception>
        public void Execute(object? parameter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("DateTime", this.TrainingMasterViewModel.TrainingDateTimeSelected);
            // トレーニングマスタ登録画面に遷移
            Shell.Current.GoToAsync(nameof(TrainingMasterAddView), parameters);
        }
    }
}
