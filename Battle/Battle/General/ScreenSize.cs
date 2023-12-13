using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.General
{
    internal class ScreenSize
    {
        public static void ShowSize()
        {
            var horizontal = string.Empty.PadLeft(254, '░');

            Console.SetCursorPosition(0, 0);
            UI.ConsoleWriter.Color(horizontal, ConsoleColor.White);

            Console.SetCursorPosition(0, 1);
            WriteVertical(64, "░", ConsoleColor.White);

            UI.ConsoleWriter.Color(horizontal, ConsoleColor.White);
            Console.SetWindowSize(256, 64);
            Console.SetWindowPosition(0, 0);
            Console.SetCursorPosition(100, 32);

            //UI.ConsoleWriter.Slow("Rescale it to your prefered size. Press any button when you are done...", 0, ConsoleColor.Cyan);
        }
        private static void WriteVertical(int count, string text, ConsoleColor color) 
        {
            for (int i = 0; i < count; i++)
            {
                UI.ConsoleWriter.Color(text, color);
                Console.SetCursorPosition(Console.CursorLeft-1, Console.CursorTop+1);
            }
        }
    }
}
