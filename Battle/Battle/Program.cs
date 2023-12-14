using Battle.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle
{
    internal class Program
    {
        private static DateTime start = DateTime.Now;
        private static DateTime presentTime = DateTime.Now;
        private static bool presentIsPresent = true;
        static void Main(string[] args)
        {
            //resize and demonstrate
            General.ScreenSize.ShowSize();
            Console.SetCursorPosition(0, 0);
            UI.DisplayImage.General(0, 0);

            //Inventory List
            List<(int slot, int id)> inventory = new List<(int slot, int id)>();
            IdGenerator idGenerator = new IdGenerator();

            while (true)
            {
                if (presentIsPresent)
                    Present.PresentIsPresent.DisplayPresent();

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    presentIsPresent = Present.OpenPresent.Open();
                }
                Present.CheckToGenerate.CheckTime(presentTime);
            }
        }
    }
}
