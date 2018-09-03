using InterviewCalender.InterviewCalender.Common;
using InterviewCalender.InterviewCalender.Data.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Business.Roles
{
    public sealed class Interviewer : User
    {
        public override void SetTimeSlot(int userId, DateTime startTime, DateTime endTime)
        {
            if (!User.GetInstance(EnumRole.Interviewer).IsUserExists(userId))
            {
                throw new Exception("No user exists with given id, please re enter user id");
            }
            if (endTime <= startTime)
            {
                throw new Exception("Start date must be smaller than end date!");
            }

            if (startTime.Year != endTime.Year || startTime.DayOfYear != endTime.DayOfYear)
            {
                throw new Exception("Day of start date and end date must be the same!");
            }

            if (startTime.Minute > 0 || endTime.Minute > 0)
            {
                throw new Exception("Start and end times must be beginning of hours.");
            }

            if (startTime < DateTime.Today || endTime < DateTime.Today)
            {
                throw new Exception("Start/end date cannot be earlier than today!");
            }

            AvailableTimeSlotsDataRepository.SetTimeSlot(userId, startTime, endTime);
        }

        public override int CreateUser(string userName)
        {
            return UserDataRepository.CreateUser(userName, (short)EnumRole.Interviewer);

        }

        public override string ChoiceSelection()
        {
            return "Please make a choice : \n 0 - Exit \n 1 - Create a User \n 2 - Available slot entry \n 3 - Query Availability";
        }

        public override bool IsUserExists(int userId)
        {
            return UserDataRepository.IsUserExists(userId, (short)EnumRole.Interviewer);
        }
    }
}
