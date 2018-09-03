using InterviewCalender.InterviewCalender.Business.Roles;
using InterviewCalender.InterviewCalender.Common;
using InterviewCalender.InterviewCalender.Data.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Business
{
    public class TimeSlot
    {
        internal void QueryAvailabilities(int candidateIdValue, string interwieverIDValues)
        {
            if(!User.GetInstance(EnumRole.Candidate).IsUserExists(candidateIdValue))
            {
                throw new Exception("Candidate user id is wrong, no such user exists!");
            }

            if (!User.IsInterviewerUsersExists(interwieverIDValues))
            {
                throw new Exception("Interviewer user id is wrong, no such user exists!");
            }

            int[] ids = Util.GetIds(interwieverIDValues);
            AvailableTimeSlotsDataRepository.QueryAvailabilities(candidateIdValue, ids);
            
        }
    }
}
