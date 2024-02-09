using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Models.DB;
using TrainingApp.ViewModels;

namespace TrainingApp.Commands
{
    class DeleteDBTrainingMasterCommand : ICommand
    {
        private TrainingMasterViewModel TrainingMasterViewModel { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DeleteDBTrainingMasterCommand(TrainingMasterViewModel viewModel) 
        {
            this.TrainingMasterViewModel = viewModel;
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
            TrainingMaster trainingMaster = (TrainingMaster)parameter;
            using (TrainingDBContext context = new TrainingDBContext())
            {
                trainingMaster.Delete(context);
                // トレーニングマスタ再設定
                this.TrainingMasterViewModel.SetTrainingMaster();
            }
        }
    }
}
