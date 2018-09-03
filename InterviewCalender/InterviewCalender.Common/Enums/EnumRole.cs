using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Common
{
    public enum EnumRole
    {
        [Description("Interviewer")]
        Interviewer = 1,
        [Description("Candidate")]
        Candidate = 2
    }
}
