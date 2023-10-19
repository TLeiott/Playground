using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinatensystem.CMD_Line
{
    internal class Circle
    {
        public static void DisplayCircle(List<(int, int)> mainList, int size, string[] latest, List<(int, int)> lastList)
        {
            lastList.Clear();
            UI.Coordinate_System.Render(mainList, size);
            int positionX = 0;
            int positionY = 0;
            float r = 0;

            //X Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Kreis: ");
            Formating.ConsoleWriter.Color("[y = Sqrt(r²-(x-", ConsoleColor.DarkGreen);//y = Sqrt(r²-(x-a)²)+b
            Formating.ConsoleWriter.Color("a", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color(")²)+b]", ConsoleColor.DarkGreen);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die X-Coordinate des Kreises ein(0-{size*3}):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[1]; } else if (latest[1] != "") { latest[1] = input; }
                if (int.TryParse(input, out positionX) && positionX <= size*3) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size*3} ein:", ConsoleColor.Red);
            }

            //Y Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Sinus: ");
            Formating.ConsoleWriter.Color($"[y = Sqrt(r²-(x-{positionX})²)+", ConsoleColor.DarkGreen);//y = Sqrt(r²-(x-a)²)+b
            Formating.ConsoleWriter.Color("b", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color("]", ConsoleColor.DarkGreen);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die Y-Coordinate des Kreises ein(0-{size}):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[2]; } else if (latest[2] != "") { latest[2] = input; }
                if (int.TryParse(input, out positionY) && positionY <= size) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //radius Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Kreis: ");
            Formating.ConsoleWriter.Color("[y = Sqrt(", ConsoleColor.DarkGreen);//y = Sqrt(r²-(x-a)²)+b
            Formating.ConsoleWriter.Color("r", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color($"²-(x-{positionX})²)+{positionY}]", ConsoleColor.DarkGreen);
            Formating.ConsoleWriter.LineColor($" Bitte gieb den Radius des Kreises ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[3]; } else if (latest[3] != "") { latest[3] = input; }
                if (input.Contains(",")) { input = input.Replace(",", "."); }
                if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out r)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Zahl ein:", ConsoleColor.Red);
            }

            //erstellen der Sinsukurve
            for (float x = 0; x < size * 3 - 1; x += (1f / size / 2))
            {
                int y = (int)Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x - positionX + 1, 2)) + positionY - 1;
                if (y < size && y >= 0)
                {
                    if (!mainList.Contains((Convert.ToInt32(Math.Round(x)), (int)y)))
                    {
                        mainList.Add((Convert.ToInt32(Math.Round(x)), (int)y));
                        lastList.Add((Convert.ToInt32(Math.Round(x)), (int)y));
                        UI.Coordinate_System.RenderSingle(Convert.ToInt32(Math.Round(x)), (int)y, size, ConsoleColor.Blue);
                    }
                }
                y = (int)(-Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x - positionX + 1, 2)) + positionY);
                if (y < size && y >= 0)
                {
                    if (!mainList.Contains((Convert.ToInt32(Math.Round(x)), (int)y)))
                    {
                        mainList.Add((Convert.ToInt32(Math.Round(x)), (int)y));
                        lastList.Add((Convert.ToInt32(Math.Round(x)), (int)y));
                        UI.Coordinate_System.RenderSingle(Convert.ToInt32(Math.Round(x)), (int)y, size, ConsoleColor.Blue);
                    }
                }
            }
        }
    }
}
