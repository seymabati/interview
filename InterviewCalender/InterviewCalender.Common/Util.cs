using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCalender.InterviewCalender.Common
{
    public class Util
    {
        internal static int[] GetIds(string interviewerIds)
        {
            List<int> intIds = new List<int>();
            if (interviewerIds.Contains(","))
            {
                string[] ids = interviewerIds.Split(',');
                foreach (string id in ids)
                {
                    intIds.Add(int.Parse(id));
                }
            }
            else
            {
                intIds.Add(int.Parse(interviewerIds));
            }

            return intIds.ToArray();
        }
    }
}
