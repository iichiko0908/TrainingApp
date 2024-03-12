using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Models
{
    class TrainingRecordGraphModel
    {
        /// <summary>
        /// 集計方法
        /// </summary>
        public string AggregateType { get; set; } = string.Empty;
        /// <summary>
        /// 日付
        /// </summary>
        public string DateType { get; set; } = string.Empty;

    }
}
