using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Models
{
    internal class TrainingRecordAddModel
    {
        /// <summary>
        /// 重さ合計
        /// </summary>
        public double SumWeight { get; set; }
        /// <summary>
        /// 回数合計
        /// </summary>
        public int SumNumberOfTimes {  get; set; }
    }
}
