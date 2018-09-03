using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DataModel
{
    public class Program
    {
        private static SQLiteConnection sql_con;

        public static void Main(string[] args)
        {

            SQLiteConnection.CreateFile("InterviewCalenderDB.sqlite");
            SetConnection();
            sql_con.Open();

            string createInterviewers = "CREATE TABLE USERS (ID INTEGER PRIMARY KEY AUTOINCREMENT, USER_NAME VARCHAR(100), ROLE SHORT)";
            SQLiteCommand commandI = new SQLiteCommand(createInterviewers, sql_con);
            commandI.ExecuteNonQuery();

            string interviewTimeSlots = "CREATE TABLE AVAILABLE_TIME_SLOTS (ID INTEGER PRIMARY KEY AUTOINCREMENT, USER_ID INTEGER, START_TIME DATETIME, END_TIME DATETIME)";
            SQLiteCommand commandIT = new SQLiteCommand(interviewTimeSlots, sql_con);
            commandIT.ExecuteNonQuery();

            string candidateTimeSlots = "CREATE TABLE REQUESTED_TIME_SLOTS (ID INTEGER PRIMARY KEY AUTOINCREMENT, USER_ID INTEGER, START_TIME DATETIME, END_TIME DATETIME)";
            SQLiteCommand commandCT = new SQLiteCommand(candidateTimeSlots, sql_con);
            commandCT.ExecuteNonQuery();

        }

        private static void SetConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=InterviewCalenderDB.sqlite;Version=3;");
        }
    }
}
