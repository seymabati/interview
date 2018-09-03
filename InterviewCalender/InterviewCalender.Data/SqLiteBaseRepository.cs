using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Data
{
    public static class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return "InterviewCalenderDB.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            return new SQLiteConnection("Data Source=" + DbFile);
        }

        public static void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
            }
        }
    }
}
