using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.Present
{
    internal class OpenPresent
    {
        public static bool Open()
        {
            Console.Clear();
            UI.DisplayImage.General(0, 0);
            return false;
        }
    }
}
