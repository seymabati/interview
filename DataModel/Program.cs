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

            //string dropTable1 = "DROP TABLE interviewers";
            //SQLiteCommand cmd1 = new SQLiteCommand(dropTable1, sql_con);
            //cmd1.ExecuteNonQuery();
            //string dropTable2 = "DROP TABLE CANDIDATES";
            //SQLiteCommand cmd2 = new SQLiteCommand(dropTable2, sql_con);
            //cmd2.ExecuteNonQuery();
            //string dropTable3 = "DROP TABLE AVAILABLE_TIME_SLOTS";
            //SQLiteCommand cmd3 = new SQLiteCommand(dropTable3, sql_con);
            //cmd3.ExecuteNonQuery();

            string createInterviewers = "CREATE TABLE USERS (ID INTEGER PRIMARY KEY AUTOINCREMENT, USER_NAME VARCHAR(100), ROLE SHORT)";
            SQLiteCommand commandI = new SQLiteCommand(createInterviewers, sql_con);
            commandI.ExecuteNonQuery();

            //string createCandidates = "CREATE TABLE CANDIDATES (ID INTEGER PRIMARY KEY AUTOINCREMENT, USER_NAME VARCHAR(100))";
            //SQLiteCommand commandC = new SQLiteCommand(createCandidates, sql_con);
            //commandC.ExecuteNonQuery();

            string interviewTimeSlots = "CREATE TABLE AVAILABLE_TIME_SLOTS (ID INTEGER PRIMARY KEY AUTOINCREMENT, USER_ID INTEGER, START_TIME DATETIME, END_TIME DATETIME)";
            SQLiteCommand commandIT = new SQLiteCommand(interviewTimeSlots, sql_con);
            commandIT.ExecuteNonQuery();

            string candidateTimeSlots = "CREATE TABLE REQUESTED_TIME_SLOTS (ID INTEGER PRIMARY KEY AUTOINCREMENT, USER_ID INTEGER, START_TIME DATETIME, END_TIME DATETIME)";
            SQLiteCommand commandCT = new SQLiteCommand(candidateTimeSlots, sql_con);
            commandCT.ExecuteNonQuery();

            //string sql = "insert into highscores (name, score) values ('Me', 3000)";
            //SQLiteCommand command = new SQLiteCommand(sql, sql_con);
            //command.ExecuteNonQuery();
            //sql = "insert into highscores (name, score) values ('Myself', 6000)";
            //command = new SQLiteCommand(sql, sql_con);
            //command.ExecuteNonQuery();
            //sql = "insert into highscores (name, score) values ('And I', 9001)";
            //command = new SQLiteCommand(sql, sql_con);
            //command.ExecuteNonQuery();

            //string sql2 = "select * from highscores order by score desc";
            //SQLiteCommand command2 = new SQLiteCommand(sql2, sql_con);
            //SQLiteDataReader reader = command2.ExecuteReader();
            //while (reader.Read())
            //    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

            Console.ReadLine();
        }

        private static void SetConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=InterviewCalenderDB.sqlite;Version=3;");
        }
    }
}
