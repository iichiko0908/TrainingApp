using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Models.DB;

namespace TrainingApp.Models.Group
{
    internal class TrainingRecordListGroup : List<TrainingRecordList>
    {
        /// <summary>
        /// 種目名
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// トレーニングマスタID
        /// </summary>
        public int TrainingMasterId { get; set; }
        /// <summary>
        /// 重さ
        /// </summary>
        public double SumWeight { get; set; }
        /// <summary>
        /// 回数
        /// </summary>
        public int SumNumberOfTimes { get; set; }

        public TrainingRecordListGroup(string eventName, int trainingMasterId, List<TrainingRecordList> list) : base(list)
        {
            this.EventName = eventName;
            this.TrainingMasterId = trainingMasterId;
            this.SumWeight = list.Sum(l => l.Weight ?? 0);
            this.SumNumberOfTimes = list.Sum(l => l.NumberOfTimes ?? 0);
        }
    }
}
