using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.ViewModels;

namespace TrainingApp.Commands
{
    internal class ConcatCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        /// <summary>
        /// 画面項目（参照）
        /// </summary>
        private SampleViewModel SampleViewModel{ get; set; }

        public ConcatCommand(SampleViewModel viewModel)
        {
            this.SampleViewModel = viewModel;
        }

        /// <summary>
        /// コマンドが利用可能かどうか
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(this.SampleViewModel.Value1) && !string.IsNullOrEmpty(this.SampleViewModel.Value2);
        }
        /// <summary>
        /// 実行時の処理
        /// </summary>
        /// <param name="parameter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Execute(object? parameter)
        {
            this.SampleViewModel.ResultValue = this.SampleViewModel.Value1 + this.SampleViewModel.Value2;
        }
        /// <summary>
        /// CanExecute実行
        /// </summary>
        public void RiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
