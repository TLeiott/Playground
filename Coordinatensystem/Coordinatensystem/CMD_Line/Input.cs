using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Coordinatensystem.CMD_Line
{
    internal class Input
    {
        public static void MainCMD(List<(int, int)> mainList, List<(int, int)> lastList, int size)
        {
            //input
            string[] latest = new string[10];
            bool run = true;
            bool messageOverwrite = false;
            while (run)
            {
                UI.Coordinate_System.Render(mainList, size);
                ClearInputField(size, messageOverwrite);
                Console.SetCursorPosition(0, size + 1);
                Console.SetWindowPosition(0, 0);
                Thread.Sleep(100);

                //startmessage overwrite
                if (messageOverwrite) { messageOverwrite = false; }
                else { Formating.ConsoleWriter.LineColor("Please input a Command(h for help):"); Console.CursorVisible = true; }

                Console.SetCursorPosition(0, size + 2);
                string input = Console.ReadLine();
                if (input == "") { input = latest[0]; } else if (latest[0] != "" && input != "Clear" && input != "clear" && input != "z" && input != "Z" && input != "r" && input != "R") { latest[0] = input; }
                
                //Try Smart
                if (input[0] == 'y' && input[1] == '=')
                {
                    CMD_Line.Smart.AutoDraw(input,size,mainList,lastList);
                    input = "";
                }

                //input
                switch (input)
                {
                    case "h":
                    case "H"://Help
                        messageOverwrite = UI.DisplayInfo.DisplayHelpInformation(size);
                        break;
                    case "p":
                    case "P"://Parabel
                        CMD_Line.Parabel.DisplayParabel(mainList, size, latest, lastList); break;
                    case "clear":
                    case "Clear"://Clear
                        mainList = UI.Coordinate_System.ClearList(mainList, size); messageOverwrite = true; break;
                    case "v":
                    case "V"://Vector
                        messageOverwrite = true;
                        CMD_Line.Vector.DisplayVector(mainList, size, latest, lastList);
                        break;
                    case "r":
                    case "R"://Reload
                        UI.Coordinate_System.Background(size);
                        UI.Coordinate_System.Render(mainList, size);
                        break;
                    case "q":
                    case "Q"://Quit
                        run = false;
                        break;
                    case "s":
                    case "S"://sinus
                        CMD_Line.Sinus.DisplaySinus(mainList, size, latest, lastList);
                        break;
                    case "c":
                    case "C"://Kreis
                        CMD_Line.Circle.DisplayCircle(mainList, size, latest, lastList);
                        break;
                    case "z":
                    case "Z"://undo
                        CMD_Line.UnDo.UnDoLast(mainList, lastList);
                        UI.Coordinate_System.Background(size);
                        UI.Coordinate_System.Render(mainList, size);
                        break;
                    default:
                        UI.Coordinate_System.Render(mainList, size); break;
                }
            }
            Console.Clear();
            Formating.ConsoleWriter.Slow("Vielen Dank für das nutzen meines kleinen Programmes. Ich hoffe es hat dir gefallen. Für das schließen dieses Fensters drücke eine Beliebige Taste. :)");
            Console.WriteLine();
            Formating.ConsoleWriter.Slow(" <3 ", 25, ConsoleColor.Magenta);
        }
        public static void ClearInputField(int size, bool messageOverwrite = false)
        {
            Console.SetCursorPosition(0, size + 1);

            //skip messagefield
            if (!messageOverwrite) { Console.WriteLine(string.Empty.PadLeft(250, ' ')); } else { Console.WriteLine(""); }

            Console.WriteLine(string.Empty.PadLeft(150, ' '));
            Console.SetCursorPosition(0, size + 1);
        }
    }
}
