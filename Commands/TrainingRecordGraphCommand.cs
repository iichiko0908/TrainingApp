using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingApp.Models.DB;
using TrainingApp.ViewModels;

namespace TrainingApp.Commands
{
    class TrainingRecordGraphCommand : ICommand
    {
        /// <summary>
        /// 画面項目クラス
        /// </summary>
        private TrainingRecordGraphViewModel TrainingRecordGraphViewModel { get; set; }

        public TrainingRecordGraphCommand(TrainingRecordGraphViewModel viewModel) 
        { 
            this.TrainingRecordGraphViewModel = viewModel;
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
            return true;
        }
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            TrainingMaster selectedTrainingMaster = (TrainingMaster)parameter;

            using (TrainingDBContext context = new TrainingDBContext())
            {
                // トレーニングリストを種目で取得して登録日でソート
                IEnumerable<TrainingRecordList> trainingRecordList = 
                    context.Connection.Table<TrainingRecordList>().Where(t => t.TrainingMasterId == selectedTrainingMaster.Id).OrderBy(t => t.Created);

                if (this.TrainingRecordGraphViewModel.AggregateType == "MAX重量回数")
                {
                    if (this.TrainingRecordGraphViewModel.DateType == "1日単位")
                    {
                        SetGraphMaxDay(context, trainingRecordList);
                    }
                    else if (this.TrainingRecordGraphViewModel.DateType == "1か月単位")
                    {
                        SetGraphMaxMonth(context, trainingRecordList);
                    }
                }
                else if (this.TrainingRecordGraphViewModel.AggregateType == "総平均")
                {
                    if (this.TrainingRecordGraphViewModel.DateType == "1日単位")
                    {
                        SetGraphMaxDay(context, trainingRecordList);
                    }
                    else if (this.TrainingRecordGraphViewModel.DateType == "1か月単位")
                    {
                        SetGraphMaxMonth(context, trainingRecordList);
                    }
                }
            }

        }

        #region 重量回数
        /// <summary>
        /// 1日MAX重量回数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trainingRecordList"></param>
        private void SetGraphMaxDay(TrainingDBContext context, IEnumerable<TrainingRecordList> trainingRecordList)
        {
            Dictionary<DateTime, TrainingRecordList> graphList = new Dictionary<DateTime, TrainingRecordList>();

            // 登録日でグループ
            IEnumerable<IEnumerable<TrainingRecordList>> groupListCreated = trainingRecordList.GroupBy(t => t.Created);
            // 登録日グループでループしてリストに設定
            SetMaxRecord(groupListCreated, graphList);

            // グラフ設定
            SetGraph(graphList);
        }
        /// <summary>
        /// 1か月MAX重量回数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trainingRecordList"></param>
        private void SetGraphMaxMonth(TrainingDBContext context, IEnumerable<TrainingRecordList> trainingRecordList)
        {
            Dictionary<DateTime, TrainingRecordList> graphList = new Dictionary<DateTime, TrainingRecordList>();

            // 年月でグループ
            IEnumerable<IEnumerable<TrainingRecordList>> groupListCreated = trainingRecordList.GroupBy(t => t.Created.ToString("yyyyMM"));

            // 年月グループでループしてリストに設定
            SetMaxRecord(groupListCreated, graphList);

            // グラフ設定
            SetGraph(graphList);
        }
        /// <summary>
        /// 日付ループでリストに設定
        /// </summary>
        /// <param name="groupListCreated"></param>
        /// <param name="graphList"></param>
        private void SetMaxRecord(IEnumerable<IEnumerable<TrainingRecordList>> groupListCreated, Dictionary<DateTime, TrainingRecordList> graphList)
        {
            // 日付ごとにループ
            foreach (IEnumerable<TrainingRecordList> groups in groupListCreated)
            {
                // 回数を多い順にソートした後に重さが重い順にソートして最初のレコードを取得する
                TrainingRecordList dayMaxRecord = groups.OrderByDescending(t => t.NumberOfTimes).OrderByDescending(t => t.Weight).FirstOrDefault();
                graphList.Add(dayMaxRecord.Created, dayMaxRecord);
            }
        }
        #endregion

        #region 重量総平均
        /// <summary>
        /// 1日MAX重量総平均
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trainingRecordList"></param>
        private void SetGraphMaxDayAverage(TrainingDBContext context, IEnumerable<TrainingRecordList> trainingRecordList)
        {
            Dictionary<DateTime, TrainingRecordList> graphList = new Dictionary<DateTime, TrainingRecordList>();

            // 登録日でグループ
            IEnumerable<IEnumerable<TrainingRecordList>> groupListCreated = trainingRecordList.GroupBy(t => t.Created);
            // 登録日で平均を設定
            SetAverage(groupListCreated, graphList);

            // グラフ設定
            SetGraph(graphList);
        }
        /// <summary>
        /// 1か月MAX重量総平均
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trainingRecordList"></param>
        private void SetGraphMaxMonthAverage(TrainingDBContext context, IEnumerable<TrainingRecordList> trainingRecordList)
        {
            Dictionary<DateTime, TrainingRecordList> graphList = new Dictionary<DateTime, TrainingRecordList>();

            // 年月でグループ
            IEnumerable<IEnumerable<TrainingRecordList>> groupListCreated = trainingRecordList.GroupBy(t => t.Created.ToString("yyyyMM"));
            // 年月で平均を設定
            SetAverage(groupListCreated, graphList);

            // グラフ設定
            SetGraph(graphList);
        }

        /// <summary>
        /// 日付ループでリストに設定
        /// </summary>
        /// <param name="groupListCreated"></param>
        /// <param name="graphList"></param>
        private void SetAverage(IEnumerable<IEnumerable<TrainingRecordList>> groupListCreated, Dictionary<DateTime, TrainingRecordList> graphList)
        {
            // 登録日ごとにループ
            foreach (IEnumerable<TrainingRecordList> groups in groupListCreated)
            {
                // 重さと回数の平均を設定する
                TrainingRecordList dayAverageTrainingRecordList = new TrainingRecordList()
                {
                    Created = groups.First().Created,
                    Id = groups.First().Id,
                    TrainingMasterId = groups.First().TrainingMasterId,
                    // 重さの平均
                    Weight = groups.Average(g => g.Weight),
                    // 回数の平均
                    NumberOfTimes = ((int?)groups.Average(g => g.NumberOfTimes))
                };

                graphList.Add(dayAverageTrainingRecordList.Created, dayAverageTrainingRecordList);
            }
        }
        #endregion


        /// <summary>
        /// グラフ設定
        /// </summary>
        /// <param name="graphList"></param>
        private void SetGraph(Dictionary<DateTime, TrainingRecordList> graphList)
        {
            // *************************************
            // Y軸設定
            // *************************************
            // Y軸(重さ)
            List<double>? setSeriesWeightList = new List<double>();
            foreach (double weight in graphList.Values.Select(g => g.Weight))
            {
                setSeriesWeightList.Add(weight);
            }
            // Y軸(回数)
            List<int>? setSeriesNumberOfTimes = new List<int>();
            foreach (int numberOfTimes in graphList.Values.Select(g => g.NumberOfTimes))
            {
                setSeriesNumberOfTimes.Add(numberOfTimes);
            }

            // Y軸画面表示
            TrainingRecordGraphViewModel.Series = new ObservableCollection<ISeries>
            {
                new LineSeries<int>
                {
                    Values = setSeriesNumberOfTimes.ToArray()
                },
                new LineSeries<double>
                {
                    Values = setSeriesWeightList.ToArray()
                },
            };

            // *************************************
            // X軸設定
            // *************************************
            // X軸(日付)
            List<string>? setAxisList = new List<string>();
            foreach (DateTime date in graphList.Values.Select(g => g.Created))
            {
                setAxisList.Add(date.ToString("MM/dd"));
            }


            // X軸画面表示
            TrainingRecordGraphViewModel.XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Labels = setAxisList.ToArray(),
                    Name = "Date"
                }
            };
        }
    }
}
