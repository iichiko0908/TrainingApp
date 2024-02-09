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
    class CreateDBTrainingMasterCommand : ICommand
    {
        /// <summary>
        /// 画面項目クラス
        /// </summary>
        private TrainingMasterAddViewModel TrainingMasterAddViewModel { get; set; }

        public CreateDBTrainingMasterCommand(TrainingMasterAddViewModel viewModel) 
        { 
            this.TrainingMasterAddViewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;
        /// <summary>
        /// 判定の呼び出し
        /// </summary>
        public void RiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        public bool CanExecute(object? parameter)
        {
            if (!string.IsNullOrEmpty(this.TrainingMasterAddViewModel.Part) && !string.IsNullOrEmpty(this.TrainingMasterAddViewModel.EventName))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            using (TrainingDBContext context = new TrainingDBContext())
            {
                // 画面で設定したマスタ
                TrainingMaster trainingMaster = new TrainingMaster() { Part = this.TrainingMasterAddViewModel.Part, EventName = this.TrainingMasterAddViewModel.EventName };

                if (0 < this.TrainingMasterAddViewModel.TrainingMasterId)
                {
                    trainingMaster.Id = this.TrainingMasterAddViewModel.TrainingMasterId;
                    // 更新
                    trainingMaster.Update(context);
                }
                else
                {
                    // 登録
                    trainingMaster.Insert(context);
                }


                // 画面クリア
                this.TrainingMasterAddViewModel.Part = string.Empty;
                this.TrainingMasterAddViewModel.EventName = string.Empty;
                this.TrainingMasterAddViewModel.TrainingMasterId = 0;
                this.TrainingMasterAddViewModel.Order = null;
            }
        }
    }
}
