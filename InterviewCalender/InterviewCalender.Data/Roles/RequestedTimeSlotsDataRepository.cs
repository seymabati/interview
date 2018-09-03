using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Data.Roles
{
    public class RequestedTimeSlotsDataRepository
    {
        public static void RequestTimeSlot(int userId, DateTime startTime, DateTime endTime)
        {
            if(CheckSameSlotExists(userId, startTime, endTime))
            {
                return;
            }

            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();

                string sql = "insert into REQUESTED_TIME_SLOTS (user_id, start_time, end_time) values( @userId, @startTime , @endTime )";

                SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@startTime", startTime);
                cmd.Parameters.AddWithValue("@endTime", endTime);
                cmd.ExecuteNonQuery();
            }
        }

        private static bool CheckSameSlotExists(int userId, DateTime startTime, DateTime endTime)
        {
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                string sql = "select * from REQUESTED_TIME_SLOTS where user_id = @userId and start_time = @startTime and end_time = @endTime";
                SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@startTime", startTime);
                cmd.Parameters.AddWithValue("@endTime", endTime);

                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    return true;
                else
                    return false;

            }
        }
    }
    
}
