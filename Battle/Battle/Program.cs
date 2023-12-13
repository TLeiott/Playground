using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            General.ScreenSize.ShowSize();
            //Console.Read();
            Console.SetCursorPosition(0, 0);
            UI.DisplayImported.WriteFromFile("Beispiel.txt");
            Console.Read();
        }
    }
}
