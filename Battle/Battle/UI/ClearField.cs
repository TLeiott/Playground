using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.UI
{
    internal class ClearField
    {
        public static void ClearPresent()
        {
            var horizontal = string.Empty.PadLeft(33, ' ');
            Console.SetCursorPosition(2, 20);
            for (int i = 0; i < 13; i++)    
            {
                Console.Write(horizontal);
                Console.SetCursorPosition(Console.CursorLeft-horizontal.Length, Console.CursorTop+1);
            }
            Console.CursorVisible = false;
        }
    }
}
