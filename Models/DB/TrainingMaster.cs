using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Models.DB
{
    [Table(nameof(TrainingMaster))]
    class TrainingMaster
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column(nameof(Id))]
        public int Id { get; set; }
        /// <summary>
        /// 部位
        /// </summary>
        [Column(nameof(Part))]
        public string Part { get; set; } = string.Empty;
        /// <summary>
        /// 種目名
        /// </summary>
        [Column(nameof(EventName))]
        public string EventName { get; set; } = string.Empty;

        /// <summary>
        /// 並び順
        /// </summary>
        [Column(nameof(Order))]
        public int? Order {  get; set; }

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
                context.Connection.BeginTransaction();
                status = context.Connection.Insert(this);
                context.Connection.Commit();
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
                context.Connection.BeginTransaction();
                context.Connection.Update(this);
                context.Connection.Commit();
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
                context.Connection.BeginTransaction();
                context.Connection.Delete(this);
                context.Connection.Commit();
            }
            catch
            {
                context.Connection.Rollback();
                throw;
            }
        }
    }
}
