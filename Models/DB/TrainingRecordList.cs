using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Models.DB
{
    class TrainingRecordList
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column(nameof(Id))]
        public int Id { get; set; }
        /// <summary>
        /// トレーニングマスタID
        /// </summary>
        [Column(nameof(TrainingMasterId))]
        public int TrainingMasterId { get; set; }
        /// <summary>
        /// セット数
        /// </summary>
        [Column(nameof(SetNumber))]
        public int SetNumber { get; set; }
        /// <summary>
        /// 重さ
        /// </summary>
        [Column(nameof(Weight))]
        public double? Weight { get; set; }
        /// <summary>
        /// 回数
        /// </summary>
        [Column(nameof(NumberOfTimes))]
        public int? NumberOfTimes { get; set; }

        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// DB登録
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public int Insert(TrainingDBContext context)
        {
            int status = 0;
            try
            {
                status = context.Connection.Insert(this);
            }
            catch
            {
                context.Connection.Rollback();
                throw;
            }

            return status;
        }
        /// <summary>
        /// DB更新
        /// </summary>
        /// <param name="context"></param>
        public void Update(TrainingDBContext context)
        {
            try
            {
                context.Connection.Update(this);
            }
            catch
            {
                context.Connection.Rollback();
                throw;
            }
        }
        /// <summary>
        /// DB削除
        /// </summary>
        /// <param name="context"></param>
        public void Delete(TrainingDBContext context)
        {
            try
            {
                context.Connection.Delete(this);
            }
            catch
            {
                context.Connection.Rollback();
                throw;
            }
        }
    }
}
