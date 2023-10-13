using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coordinatensystem.UI
{
    internal class Coordinate_System
    {
        public static void Background(int size)
        {
            for (int i = 0; i < size; i++)
            {
                // for animation
                Thread.Sleep(0);

                //anzeigen
                var s = string.Empty.PadLeft(size * 3, '░');
                Console.SetCursorPosition(1, size - i - 1);
                Formating.ConsoleWriter.Color(s, ConsoleColor.Gray);
            }
            //Randmarkierungen Vertikal
            Console.SetCursorPosition(0, size);
            for (int i = 0; i <= size; i++)
            {
                if (i <= 4) { Console.Write(i); }
                else if (i % 5 == 0 && i % 10 != 0) { Formating.ConsoleWriter.Color("5", ConsoleColor.White); }
                else if (i % 10 == 0) { Formating.ConsoleWriter.Color("█", ConsoleColor.White); }
                else { Formating.ConsoleWriter.Color("║", ConsoleColor.Gray); }

                if (size != i) { Console.SetCursorPosition(0, Console.CursorTop - 1); }
            }
            //Randmarkierungen Horizontal
            Console.SetCursorPosition(0, size);
            for (int i = 0; i <= size * 3; i++)
            {
                if (i <= 4) { Console.Write(i); }
                else if (i % 5 == 0 && i % 10 != 0) { Formating.ConsoleWriter.Color("5", ConsoleColor.White); }
                else if (i % 10 == 0) { Formating.ConsoleWriter.Color("█", ConsoleColor.White); }
                else { Formating.ConsoleWriter.Color("═", ConsoleColor.Gray); }
            }
        }
        public static void Render(List<(int, int)> mainList, int size, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            foreach (var value in mainList)
            {
                Console.SetCursorPosition(value.Item1 + 1, -value.Item2 + size - 1);
                Formating.ConsoleWriter.LineColor("█", color);
            }
        }
        public static void RenderSingle(int x, int y, int size, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x + 1, -y + size - 1);
            Formating.ConsoleWriter.LineColor("█", color);
            Thread.Sleep(1);
        }
        public static List<(int, int)> ClearList(List<(int, int)> mainList, int size)
        {
            Background(size);
            mainList.Clear();
            CMD_Line.Input.ClearInputField(size, false);
            Formating.ConsoleWriter.LineColor("List cleared succesfully. Write your next command:", ConsoleColor.Green);
            return mainList;
        }
    }
}
