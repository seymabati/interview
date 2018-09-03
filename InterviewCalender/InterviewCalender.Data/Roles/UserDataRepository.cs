using InterviewCalender.InterviewCalender.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Data.Roles
{
    public class UserDataRepository
    {
        public static int CreateUser(string userName, short role)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Please enter a valid user name!");
            }

            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                string sql = "insert into users (user_name, role) values(@user_name, @role)";
                SQLiteCommand command = new SQLiteCommand(sql, cnn);
                command.Parameters.AddWithValue("@user_name",userName);
                command.Parameters.AddWithValue("@role", role);
                command.ExecuteNonQuery();

                string sqlRowId = "SELECT last_insert_rowid()";
                SQLiteCommand cmd = new SQLiteCommand(sqlRowId, cnn);
                return Convert.ToInt32(cmd.ExecuteScalar()); 
            }
        }

        internal static bool IsUserExists(int userId, short roleID)
        {
            using(var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                string sql = "select * from users where id = @user_id and role = @roleId";
                SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@roleId", roleID);
                var returnedRows = cmd.ExecuteScalar();
                if (returnedRows == null)
                    return false;
                else
                    return true;
            }
        }

        internal static bool IsUsersExists(int[] interwieverIDValues, short role)
        {
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();

                StringBuilder sb = new StringBuilder();
                int i = 1;
                foreach (int id in interwieverIDValues)
                {
                    sb.Append("@userId" + i.ToString() + ",");
                    i++;
                }
                string inClause = sb.ToString().Substring(0,sb.ToString().Length -1);
                string sql = "select * from users where id in ("+ inClause + ") and role = @role";
                SQLiteCommand cmd = new SQLiteCommand(sql, cnn);
                i = 1;
                foreach (int id in interwieverIDValues)
                {
                    cmd.Parameters.AddWithValue("@userId" + i.ToString(), id);
                    i++;
                }
                cmd.Parameters.AddWithValue("@role", role);
                var returnedRows = cmd.ExecuteScalar();
                if (returnedRows == null)
                    return false;
                else
                    return true;
            }
        }
    }
}
