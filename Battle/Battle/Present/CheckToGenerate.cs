using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Present
{
    internal class CheckToGenerate
    {
        public static bool CheckTime(DateTime start)
        {
            return DateTime.Now < start.AddSeconds(30);
        }
    }
}
