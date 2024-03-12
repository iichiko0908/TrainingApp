using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Commands;
using TrainingApp.Models;
using TrainingApp.Models.DB;

namespace TrainingApp.ViewModels
{
    internal class TrainingRecordGraphViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 変更通知
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        private TrainingRecordGraphModel model { get; set; }

        /// <summary>
        /// 集計方法
        /// </summary>
        public string AggregateType
        {
            get
            {
                return model.AggregateType;
            }
            set
            {
                model.AggregateType = value;
                NotifyPropertyChanged();
            }
        }
        /// <summary>
        /// 日付
        /// </summary>
        public string DateType
        {
            get
            {
                return model.DateType;
            }
            set
            {
                model.DateType = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 日付
        /// </summary>
        public string EventName
        {
            get
            {
                return model.DateType;
            }
            set
            {
                model.DateType = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// トレーニングマスタ
        /// </summary>
        public List<TrainingMaster> TrainingMasterList { get; set; }


        public ObservableCollection<ISeries> _series { get; set; }
        /// <summary>
        /// Y軸
        /// </summary>
        public ObservableCollection<ISeries> Series {
            get 
            {
                return _series;
            }
            set
            {
                _series = value;
                NotifyPropertyChanged();
            }    
        }

        public ObservableCollection<Axis> _xAxes { get; set; }

        /// <summary>
        /// X軸
        /// </summary>
        public ObservableCollection<Axis> XAxes
        {
            get
            {
                return _xAxes;
            }
            set
            {
                _xAxes = value;
                NotifyPropertyChanged();
            }
        }

        public LabelVisual Title { get; set; } =
            new LabelVisual
            {
                Text = "Traning Graph",
                TextSize = 20,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };

        /// <summary>
        /// トレーニングレコードグラフコマンド
        /// </summary>
        public TrainingRecordGraphCommand TrainingRecordGraphCommand {  get; set; }

        public ICommand ChangePickerCommand
        {
            get
            {
                return new Command(
                    execute: (object? parameter) =>
                    {
                        //TrainingMaster selectedTrainingMaster = (TrainingMaster)parameter;
                        //parameter = null;

                        //TrainingMasterList = new List<TrainingMaster>();
                        //SetTrainingMaster();
                    }
                );
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TrainingRecordGraphViewModel()
        {
            model = new TrainingRecordGraphModel();
            TrainingRecordGraphCommand = new TrainingRecordGraphCommand(this);

            // トレーニングマスタ取得
            SetTrainingMaster();


        }

        /// <summary>
        /// トレーニングマスタ取得
        /// </summary>
        private void SetTrainingMaster()
        {
            using (TrainingDBContext context = new TrainingDBContext())
            {
                TrainingMasterList = context.Connection.Table<TrainingMaster>().ToList();

            }
        }

        /// <summary>
        /// プロパティの変更通知
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
