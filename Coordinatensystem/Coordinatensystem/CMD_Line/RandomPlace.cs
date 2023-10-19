using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinatensystem.CMD_Line
{
    internal class RandomPlace
    {
        public static void DisplayRandom(List<(int, int)> mainList, int size, string[] latest, List<(int, int)> lastList)
        {
            Random random = new Random();
            lastList.Clear();
            UI.Coordinate_System.Render(mainList, size);
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            int factor = 0;

            //X Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Random: ");
            Formating.ConsoleWriter.Color($"[(", ConsoleColor.Magenta);//Random: [(x1|y1)x(x2|y2)]<H>
            Formating.ConsoleWriter.Color("x1", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color($"|y1)x(x2|y2)]<H>", ConsoleColor.Magenta);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die start X-Coordinate ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[1]; } else if (latest[1] != "") { latest[1] = input; }
                if (int.TryParse(input, out x1)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine Zahl ein:", ConsoleColor.Red);
            }

            //Y Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Random: ");
            Formating.ConsoleWriter.Color($"[({x1}|", ConsoleColor.Magenta);//Random: [(x1|y1)x(x2|y2)]<H>
            Formating.ConsoleWriter.Color("y1", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color($")x(x2|y2)]<H>", ConsoleColor.Magenta);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die start Y-Coordinate ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[2]; } else if (latest[2] != "") { latest[2] = input; }
                if (int.TryParse(input, out y1)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine Zahl ein:", ConsoleColor.Red);
            }

            //X Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Random: ");
            Formating.ConsoleWriter.Color($"[({x1}|{y1})x(", ConsoleColor.Magenta);//Random: [(x1|y1)x(x2|y2)]<H>
            Formating.ConsoleWriter.Color("x2", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color($"|y2)]<H>", ConsoleColor.Magenta);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die end X-Coordinate ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[3]; } else if (latest[3] != "") { latest[3] = input; }
                if (int.TryParse(input, out x2)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine Zahl ein:", ConsoleColor.Red);
            }

            //Y Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Random: ");
            Formating.ConsoleWriter.Color($"[({x1}|{y1})x({x2}|", ConsoleColor.Magenta);//Random: [(x1|y1)x(x2|y2)]<H>
            Formating.ConsoleWriter.Color("y2", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color($")]<H>", ConsoleColor.Magenta);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die end Y-Coordinate ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[4]; } else if (latest[4] != "") { latest[4] = input; }
                if (int.TryParse(input, out y2)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine Zahl ein:", ConsoleColor.Red);
            }

            //Y Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Random: ");
            Formating.ConsoleWriter.Color($"[({x1}|{y1})x({x2}|{y2})]<", ConsoleColor.Magenta);//Random: [(x1|y1)x(x2|y2)]<H>
            Formating.ConsoleWriter.Color("H", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color($">", ConsoleColor.Magenta);
            Formating.ConsoleWriter.LineColor($" Bitte giebt die Anzahl der Läufe an:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[5]; } else if (latest[5] != "") { latest[5] = input; }
                if (int.TryParse(input, out factor)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine Zahl ein:", ConsoleColor.Red);
            }

            if (factor > ((x2 - x1) * (y2 - y1))) { factor = ((x2 - x1) * (y2 - y1)); }
            int x;
            int y;
            x1--;
            y1--;
            x2--;
            y2--;
            //Display it
            for (int i = 0; i < factor; i++)
            {
                x = random.Next(x1, x2);
                y = random.Next(y1, y2);
                while (mainList.Contains((x, y)))
                {
                    x = random.Next(x1, x2);
                    y = random.Next(y1, y2);
                }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"({x}|{y})", ConsoleColor.Red);
                mainList.Add((x, y));
                lastList.Add((x, y));
                UI.Coordinate_System.RenderSingle(x, y, size, ConsoleColor.Magenta);
                //CMD_Line.Input.ClearInputField(size);
                //Formating.ConsoleWriter.LineColor($"I={i}", ConsoleColor.Red);
            }
            Console.Read();
        }
    }
}
