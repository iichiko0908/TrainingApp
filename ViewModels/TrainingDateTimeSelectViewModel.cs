using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Commands;

namespace TrainingApp.ViewModels
{
    internal class TrainingDateTimeSelectViewModel : INotifyPropertyChanged
    {
        public DateTime TraningDateTime { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// 次へボタンコマンド
        /// </summary>
        public ICommand NavigationTrainingListCommand { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TrainingDateTimeSelectViewModel()
        {
            // コマンド
            this.NavigationTrainingListCommand = new NavigationTrainingListCommand(this);
            // デフォルトの日付を現在日にする
            this.TraningDateTime = DateTime.Now;
        }

        /// <summary>
        /// プロパティの変更通知を起動する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
