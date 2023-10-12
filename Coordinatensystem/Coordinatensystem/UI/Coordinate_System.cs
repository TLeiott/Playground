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
            //ClearInputField(size, false);
            Formating.ConsoleWriter.LineColor("List cleared succesfully. Write your next command:", ConsoleColor.Green);
            return mainList;
        }
        public static void MainCMD(List<(int, int)> mainList, int size)
        {
            //input
            string[] latest = new string[10];
            bool run = true;
            bool messageOverwrite = false;
            while (run)
            {
                UI.Coordinate_System.Render(mainList, size);
                //ClearInputField(size, messageOverwrite);
                Console.SetCursorPosition(0, size + 1);
                Console.SetWindowPosition(0, 0);
                Thread.Sleep(100);

                //startmessage overwrite
                if (messageOverwrite) { messageOverwrite = false; }
                else { Formating.ConsoleWriter.LineColor("Please input a Command(h for help):"); Console.CursorVisible = true; }

                Console.SetCursorPosition(0, size + 2);
                string input = Console.ReadLine();
                if (input == "") { input = latest[0]; } else if (latest[0] != "" && input != "C" && input != "c" && input != "z" && input != "Z" && input != "r" && input != "R") { latest[0] = input; }
                switch (input)
                {
                    case "h":
                    case "H"://Help
                        //messageOverwrite = DisplayHelpInformation(size);
                        break;
                    case "p":
                    case "P"://Parabel
                        //DisplayParabel(mainList, size, latest, lastList); break;
                    case "c":
                    case "C"://Clear
                        mainList = UI.Coordinate_System.ClearList(mainList, size); messageOverwrite = true; break;
                    case "v":
                    case "V"://Vector
                        messageOverwrite = true;
                        //DisplayVector(mainList, size, latest, lastList);
                        break;
                    case "r":
                    case "R"://Reload
                        Background(size);
                        Render(mainList, size);
                        break;
                    case "q":
                    case "Q"://Quit
                        run = false;
                        break;
                    case "s":
                    case "S":
                        //DisplaySinus(mainList, size, latest, lastList);
                        break;
                    case "z":
                    case "Z":
                        //DeleteLast(mainList, lastList);
                        Render(mainList, size);
                        break;
                    default:
                        Render(mainList, size); break;
                }
            }
            Console.Clear();
            Formating.ConsoleWriter.Slow("Vielen Dank für das nutzen meines kleinen Programmes. Ich hoffe es hat dir gefallen. Für das schließen dieses Fensters drücke eine Beliebige Taste. :)");
            Console.WriteLine();
            Formating.ConsoleWriter.Slow(" <3 ", 25, ConsoleColor.Magenta);
        }
    }
}
