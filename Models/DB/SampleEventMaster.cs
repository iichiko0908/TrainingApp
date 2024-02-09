using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Models.DB
{
    [Table(nameof(SampleEventMaster))]
    internal class SampleEventMaster
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column(nameof(Id))]
        public int Id { get; set; }

        [Column(nameof(WeekName))]
        public string WeekName { get; set; } = string.Empty;

        [Column(nameof(EventName))]
        public string EventName { get; set; } = string.Empty;

        [Column(nameof(Created))]
        public DateTime Created { get; set; }
        [Column(nameof(Modified))]
        public DateTime Modified { get; set; }


        /// <summary>
        /// DB登録
        /// </summary>
        /// <param name="context"></param>
        public void Insert(SampleEventMasterDbContext context)
        {
            try
            {
                context.Connection.BeginTransaction();
                context.Connection.Insert(this);
                context.Connection.Commit();
            }
            catch
            {
                context.Connection.Rollback();
                throw;
            }
        }
        /// <summary>
        /// DB更新
        /// </summary>
        /// <param name="context"></param>
        public void Update(SampleEventMasterDbContext context)
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
        public void Delete(SampleEventMasterDbContext context)
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
