using InterviewCalender.InterviewCalender.Common;
using InterviewCalender.InterviewCalender.Data.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Business.Roles
{
    public abstract class User
    {
        public static User GetInstance(EnumRole roleValue)
        {
            switch (roleValue)
            {
                case EnumRole.Interviewer:
                    return new Interviewer();
                case EnumRole.Candidate:
                    return new Candidate();
                default:
                    return null;
            }
        }

        public virtual void SetTimeSlot(int userId, DateTime avaliableSlotStart, DateTime avaliableSlotEnd)
        {

        }

        public virtual int CreateUser(string userName)
        {
            return 0;

        }

        public virtual string ChoiceSelection()
        {
            return "";
        }

        public virtual bool IsUserExists(int userId)
        {
            return false;
        }

        internal static bool IsInterviewerUsersExists(string interwieverIDValues)
        {
            int[] ids = Util.GetIds(interwieverIDValues);
            return UserDataRepository.IsUsersExists(ids, (short) EnumRole.Interviewer);
        }
    }
}
