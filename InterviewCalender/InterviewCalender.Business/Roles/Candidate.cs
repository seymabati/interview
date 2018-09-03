using InterviewCalender.InterviewCalender.Common;
using InterviewCalender.InterviewCalender.Data.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Business.Roles
{
    public sealed class Candidate : User
    {
        public override void SetTimeSlot(int userId, DateTime startTime, DateTime endTime)
        {
            if (!User.GetInstance(EnumRole.Candidate).IsUserExists(userId))
            {
                throw new Exception("No user exists with given id, please re enter user id!");
            }
            if (endTime <= startTime)
            {
                throw new Exception("Start date must be smaller than end date!");
            }
            if(startTime < DateTime.Today || endTime < DateTime.Today)
            {
                throw new Exception("Start/end date cannot be earlier than today!");
            }

            RequestedTimeSlotsDataRepository.RequestTimeSlot(userId, startTime, endTime);
        }

        public override int CreateUser(string userName)
        {
            return UserDataRepository.CreateUser(userName, (short)EnumRole.Candidate);

        }
        public override string ChoiceSelection()
        {
            return "Please make a choice : \n 0 - Exit \n 1 - Create a User \n 2 - Request slot entry \n 3 - Query availability";
        }
        public override bool IsUserExists(int userId)
        {
            return UserDataRepository.IsUserExists(userId, (short)EnumRole.Candidate);
        }
    }
}
