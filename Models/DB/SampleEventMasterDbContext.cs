using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Models.DB
{
    internal class SampleEventMasterDbContext : IDisposable
    {
        public SQLiteConnection Connection { get; set; }

        public SampleEventMasterDbContext()
        {
            string path = Path.Combine(FileSystem.Current.AppDataDirectory, "SampleEventMaster.db3");
            this.Connection = new SQLiteConnection(path);
            // DB作成
            this.Connection.CreateTable<SampleEventMaster>();
        }

        public void Dispose()
        {
            this.Connection?.Dispose();
        }
    }
}
