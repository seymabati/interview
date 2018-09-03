using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Data.Roles
{
    public class AvailableTimeSlotsDataRepository
    {
        public static void SetTimeSlot(int userId, DateTime startTime, DateTime endTime)
        {
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                DateTime endTimeTemp = startTime.AddHours(1);
                DateTime startTimeTemp = startTime;

                while(endTimeTemp <= endTime)
                {
                    if(!CheckSameSlotExists(userId, startTimeTemp, endTimeTemp))
                    {
                        string sql = "insert into AVAILABLE_TIME_SLOTS (user_id, start_time, end_time) values( @userId, @startTime , @endTime )";

                        SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@startTime", startTimeTemp);
                        cmd.Parameters.AddWithValue("@endTime", endTimeTemp);
                        cmd.ExecuteNonQuery();
                    }

                    startTimeTemp = startTimeTemp.AddHours(1);
                    endTimeTemp = endTimeTemp.AddHours(1);

                }
                cnn.Close();
            }
        }

        private static bool CheckSameSlotExists(int userId, DateTime startTime, DateTime endTime)
        {
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                string sql = "select * from AVAILABLE_TIME_SLOTS where user_id = @userId and start_time = @startTime and end_time = @endTime";
                SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@startTime", startTime);
                cmd.Parameters.AddWithValue("@endTime", endTime);


                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
        }

        public static void QueryAvailabilities(int candidateIdValue, int[] interwieverIDValues)
        {

            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                List<DateTime> candidateTimes = new List<DateTime>();
                cnn.Open();

                string sql = "select * from requested_time_slots where user_id = @candidate_id";
                SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@candidate_id", candidateIdValue);

                bool isSlotExists = false;
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime start_time = (DateTime)reader["start_time"];
                    DateTime end_time = (DateTime)reader["end_time"];

                    StringBuilder sb = new StringBuilder();
                    int i = 1;
                    foreach (int id in interwieverIDValues)
                    {
                        sb.Append("@userId" + i.ToString() + ",");
                        i++;
                    }
                    string inClause = sb.ToString().Substring(0, sb.ToString().Length - 1);
                    string sqlInt = "select * from available_time_slots tim join users usr on tim.user_id = usr.id where tim.user_id in (" + inClause + ") and tim.start_time >= @start_Time and tim.end_time <= @end_time";

                    SQLiteCommand cmdInt = new SQLiteCommand(sqlInt, cnn);
                    i = 1;
                    foreach (int id in interwieverIDValues)
                    {
                        cmdInt.Parameters.AddWithValue("@userId" + i.ToString(), id);
                        i++;
                    }
                    cmdInt.Parameters.AddWithValue("start_time", start_time);
                    cmdInt.Parameters.AddWithValue("end_time", end_time);

                    SQLiteDataReader readerInt = cmdInt.ExecuteReader();
                    
                    while (readerInt.Read())
                    {
                        isSlotExists = true;
                        Console.WriteLine("Available time slot for interviewer " + readerInt["user_id"] + " - " + readerInt["user_name"] + " : " + readerInt["start_Time"] + " - " + readerInt["end_time"]);
                    }
                }

                if (!isSlotExists)
                {
                    Console.WriteLine("No available slot exists!");
                }


            }


        }
    }
}
