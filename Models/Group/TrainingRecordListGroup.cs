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
        public string EventName { get; set; }

        public int TrainingMasterId { get; set; }

        public TrainingRecordListGroup(string eventName, int trainingMasterId, List<TrainingRecordList> list) : base(list)
        {
            this.EventName = eventName;
            this.TrainingMasterId = trainingMasterId;
        }
    }
}
