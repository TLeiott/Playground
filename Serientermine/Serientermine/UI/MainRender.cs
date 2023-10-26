using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.UI
{
    internal class MainRender
    {
        public static void Render(int step, ConsoleColor mainColor = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            while (true)
            {
                //Borders
                if (step < 1) { break; }
                var x = string.Empty.PadLeft(Console.WindowWidth - 2, '═');

                Console.SetCursorPosition(0, 0);
                UI.ConsoleWriter.Color("╔", mainColor);
                UI.ConsoleWriter.Color(x, mainColor);
                UI.ConsoleWriter.Color("╗", mainColor);
                for (int i = 0; i < Console.WindowHeight - 2; i++)
                {
                    UI.ConsoleWriter.LineColor("║", mainColor);
                }
                UI.ConsoleWriter.Color("╚", mainColor);
                UI.ConsoleWriter.Color(x, mainColor);
                UI.ConsoleWriter.Color("╝", mainColor);
                for (int i = 0; i < Console.WindowHeight - 2; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth - 1, i + 1);
                    UI.ConsoleWriter.LineColor("║", mainColor);
                }

                //InputLanes
                if (step < 2) { break; }
                Console.SetCursorPosition(0, 3);
                UI.ConsoleWriter.Color("╠", mainColor);
                UI.ConsoleWriter.Color(x, mainColor);
                UI.ConsoleWriter.Color("╣", mainColor);
                Console.SetCursorPosition(Console.WindowWidth / 2, 0);
                UI.ConsoleWriter.Color("╦", mainColor);
                Console.SetCursorPosition(Console.WindowWidth / 2, 1);
                UI.ConsoleWriter.Color("║", mainColor);
                Console.SetCursorPosition(Console.WindowWidth / 2, 2);
                UI.ConsoleWriter.Color("║", mainColor);
                Console.SetCursorPosition(Console.WindowWidth / 2, 3);
                UI.ConsoleWriter.Color("╩", mainColor);

                break;
            }
            Console.SetWindowPosition(0, 0);
            return;
        }
        public static void Error(string message)
        {
            Render(2, ConsoleColor.Red);
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 1);
            UI.ConsoleWriter.Color(message, ConsoleColor.Red);
        }
        public static void ClearInput()
        {
            Console.SetCursorPosition(2, 2);
            Console.WriteLine(string.Empty.PadLeft(Console.WindowWidth / 2 - 2, ' '));
            Console.SetCursorPosition(2, 2);
        }
        public static void DisplayInfo(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 2);
            Console.Write(string.Empty.PadLeft(Console.WindowWidth / 2 - 3, ' '));
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 2);
            UI.ConsoleWriter.Color(message, color);
        }
        public static void DisplayAdvice(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(2, 1);
            Console.Write(string.Empty.PadLeft(Console.WindowWidth / 2 - 3, ' '));
            Console.SetCursorPosition(2, 1);
            UI.ConsoleWriter.Color(message, color);
        }

        public static void Test()
        {
            Console.WriteLine();
        }
    }
}
