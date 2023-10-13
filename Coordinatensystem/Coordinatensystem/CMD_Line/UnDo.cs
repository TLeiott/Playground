using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinatensystem.CMD_Line
{
    internal class UnDo
    {
        public static void UnDoLast(List<(int, int)> mainlist, List<(int, int)> lastlist)
        {
            foreach (var value in lastlist)
            {
                mainlist.Remove((value.Item1, value.Item2));
                ///Console.SetCursorPosition(value.Item1 + 1, -value.Item2 + size - 1);
            }
        }
    }
}
