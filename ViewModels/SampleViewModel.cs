using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Commands;
using TrainingApp.Models;

namespace TrainingApp.ViewModels
{
    internal class SampleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// 画面項目クラス
        /// </summary>
        private SampleModel SampleModel {  get; set; }
        /// <summary>
        /// コマンドクラス（文字列結合ボタン）
        /// </summary>
        public ICommand ConcatCommand { get; set; }

        public SampleViewModel()
        {
            // 初期化
            SampleModel = new SampleModel();
            // コマンドクラスに自身を入れる
            ConcatCommand = new ConcatCommand(this);
        }

        /// <summary>
        /// プロパティの変更通知を起動する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Value1
        {
            get { return this.SampleModel.Value1; }
            set
            {
                if (this.SampleModel.Value1 != value)
                {
                    this.SampleModel.Value1 = value;
                    //NotifyPropertyChanged();
                    // ボタン実行可否
                    ((ConcatCommand)this.ConcatCommand).RiseCanExecuteChanged();
                }
            }
        }

        public string Value2
        {
            get { return this.SampleModel.Value2; }
            set
            {
                if (this.SampleModel.Value2 != value)
                {
                    this.SampleModel.Value2 = value;
                    //NotifyPropertyChanged();
                    // ボタン実行可否
                    ((ConcatCommand)this.ConcatCommand).RiseCanExecuteChanged();
                }
            }
        }

        private string _resultValue { get; set; } = string.Empty;
        public string ResultValue
        {
            get { return _resultValue; }
            set
            {
                if (_resultValue != value)
                {
                    _resultValue = value;
                    // Viewに反映
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
