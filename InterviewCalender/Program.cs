using InterviewCalender.InterviewCalender.Business;
using InterviewCalender.InterviewCalender.Business.Roles;
using InterviewCalender.InterviewCalender.Common;
using InterviewCalender.InterviewCalender.Data;
using System;
using System.Data.SQLite;

namespace InterviewCalender
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please select a role. Enter (1) for Interviewer, (2) for Candidate:");
            string role = Console.ReadLine();
            
            bool choiceValid = short.TryParse(role, out short roleValue);
            choiceValid = Enum.IsDefined(typeof(EnumRole), Convert.ToInt32(roleValue));

            while (!choiceValid)
            {
                Console.WriteLine("Please make a valid choice");
                role = Console.ReadLine();
                choiceValid = short.TryParse(role, out roleValue);
                choiceValid = Enum.IsDefined(typeof(EnumRole), Convert.ToInt32(roleValue));

            }

            int choiceValue = 1;
            string choiceQuestion = User.GetInstance((InterviewCalender.Common.EnumRole)(roleValue)).ChoiceSelection();

            while(choiceValue != 0)
            {
                Console.WriteLine(choiceQuestion);

                string choice = Console.ReadLine();
                choiceValid = int.TryParse(choice, out choiceValue);
                while (!choiceValid)
                {
                    Console.WriteLine("Please make a valid choice");
                    choice = Console.ReadLine();
                    choiceValid = int.TryParse(choice, out choiceValue);
                }

                switch (choiceValue)
                {
                    case 1: //Create User
                        Console.WriteLine("Enter User Name: ");
                        string userName = Console.ReadLine();
                        int createdUser = 0;
                        try
                        {
                            createdUser = User.GetInstance((InterviewCalender.Common.EnumRole)(roleValue)).CreateUser(userName);
                            Console.WriteLine("Your user id is: " + createdUser.ToString());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 2: // Set available time slots
                        Console.WriteLine("Enter User Id: ");
                        string userId = Console.ReadLine();
                        int.TryParse(userId, out int userIdValue);

                        bool continueGetSlots = true;

                        while (continueGetSlots)
                        {
                            Console.WriteLine("Enter Available Slots start time (Format dd/mm/yyyy hh:mm): ");
                            string availableSlotStartTime = Console.ReadLine();
                            DateTime.TryParse(availableSlotStartTime, out DateTime avaliableSlotStart);
                            Console.WriteLine("Enter Available Slots end time (Format dd/mm/yyyy hh:mm): ");
                            string availableSlotEndDate = Console.ReadLine();
                            DateTime.TryParse(availableSlotEndDate, out DateTime avaliableSlotEnd);

                            try
                            {
                                User.GetInstance((InterviewCalender.Common.EnumRole)(roleValue)).SetTimeSlot(userIdValue, avaliableSlotStart, avaliableSlotEnd);
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            Console.WriteLine("Continue setting slots? Enter (Y) or (N)");
                            string continueYN = Console.ReadLine();
                            if (continueYN != "Y" && continueYN != "y")
                                continueGetSlots = false;
                        }
                        break;
                    case 3: // query available time slots
                        Console.WriteLine("Enter candidate id: ");
                        string candidateId = Console.ReadLine();
                        int.TryParse(candidateId, out int candidateIdValue);

                        Console.WriteLine("Enter interwiever id(s) (if more than one put , between interviewer ids): ");
                        string interviewerIds = Console.ReadLine();

                        try
                        {
                            new TimeSlot().QueryAvailabilities(candidateIdValue, interviewerIds);
                        }catch(Exception e) {
                            Console.WriteLine(e.Message);
                        }
                        
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Please make a valid choice");
                        break;

                }

            }
            TestResult();
        }

        private static void TestResult()
        {
            string sql2 = "select * from users";
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                SQLiteCommand command2 = new SQLiteCommand(sql2, cnn);
                SQLiteDataReader reader = command2.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("Name: " + reader["user_name"] + "\tId: " + reader["id"] + "\tRole: " + reader["role"]);
            }

            string sql4 = "select * from available_time_slots";
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                SQLiteCommand command3 = new SQLiteCommand(sql4, cnn);
                SQLiteDataReader reader = command3.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("user_id: " + reader["user_id"] + "\t start time: " + reader["start_time"] + "\tend time: " + reader["end_time"]);
            }


            string sql5 = "select * from requested_time_Slots";
            using (var cnn = SqLiteBaseRepository.SimpleDbConnection())
            {
                cnn.Open();
                SQLiteCommand command3 = new SQLiteCommand(sql5, cnn);
                SQLiteDataReader reader = command3.ExecuteReader();
                Console.WriteLine("candidate requests");
                while (reader.Read())
                    Console.WriteLine("user_id: " + reader["user_id"] + "\t start time: " + reader["start_time"] + "\tend time: " + reader["end_time"]);
            }


            Console.ReadLine();
        }
    }
}
