using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Models.DB;

namespace TrainingApp.Models.Group
{
    class TrainingMasterGroup : List<TrainingMaster>
    {
        public string Part { get; set; }

        public TrainingMasterGroup(string part, List<TrainingMaster> list) : base(list)
        {
            this.Part = part;
        }
    }
}
