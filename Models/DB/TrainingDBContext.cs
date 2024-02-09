using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Models.DB
{
    class TrainingDBContext : IDisposable
    {
        public SQLiteConnection Connection { get; set; }

        public TrainingDBContext()
        {
            // 接続
            string path = Path.Combine(FileSystem.Current.AppDataDirectory, "TrainingDB.db3");
            this.Connection = new SQLiteConnection(path);
            // テーブル作成
            this.Connection.CreateTable<TrainingMaster>();
            this.Connection.CreateTable<TrainingRecordList>();
        }
        public void Dispose()
        {
            this.Connection?.Dispose();
        }

        public void BeginTrainsaction()
        {
            this.Connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                this.Connection.Commit();
            }
            catch
            {
                this.Connection.Rollback();
                throw;
            }
        }
    }
}
