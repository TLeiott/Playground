using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coordinatensystem.CMD_Line
{
    internal class Parabel
    {
        public static void DisplayParabel(List<(int, int)> mainList, int size, string[] latest, List<(int, int)> lastList)
        {
            UI.Coordinate_System.Render(mainList, size);
            lastList.Clear();
            int positionX;
            int positionY;
            decimal stretch = 0;
            //X Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Parabel: ");
            Formating.ConsoleWriter.Color("[y=a*(x-", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color("b", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color(")²+c]", ConsoleColor.Cyan);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die X-Coordinate des Scheitelpunkts ein(0-{size * 3}):");
            ///WriteLineColor("Parabel: [y=a*(x-b)²+c] Bitte gieb die X-Coordinate des Scheitelpunkts ein:", ConsoleColor.Cyan);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[1]; } else if (latest[1] != "") { latest[1] = input; }
                if (int.TryParse(input, out positionX) && positionX <= size * 3) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size * 3} ein:", ConsoleColor.Red);
            }

            //Y Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Parabel: ");
            Formating.ConsoleWriter.Color($"[y=a*(x-{positionX})²+", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color("c", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color("]", ConsoleColor.Cyan);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die Y-Coordinate des Scheitelpunkts ein(0-{size}):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[2]; } else if (latest[2] != "") { latest[2] = input; }
                if (int.TryParse(input, out positionY) && positionY <= size) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //Stauchen oder Strecken
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Parabel: ");
            Formating.ConsoleWriter.Color("[y=", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color("a", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color($"*(x-{positionX})²+{positionY}]", ConsoleColor.Cyan);
            Formating.ConsoleWriter.LineColor("  Soll die Parabel gestaucht(0.XX) oder gestreckt(XX.0) sein?:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[3]; } else if (latest[3] != "") { latest[3] = input; }
                if (input.Contains(",")) { input = input.Replace(",", "."); }
                if (decimal.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out stretch)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Zahl ein:", ConsoleColor.Red);
            }

            //erstellen der Parabel
            bool scheitelPunkt = true;
            for (float x = 0; x < size * 3 - 1; x += (1f / size))
            {
                double power = Math.Pow(x, 2);
                int y = (int)(stretch * (decimal)(Math.Pow((x - (positionX - 1)), 2)) + (positionY - 1M));
                if (y < size && y >= 0)
                {
                    if (!mainList.Contains((Convert.ToInt32(Math.Round(x)), (int)y)))
                    {
                        mainList.Add((Convert.ToInt32(Math.Round(x)), (int)y));
                        lastList.Add((Convert.ToInt32(Math.Round(x)), (int)y));
                        UI.Coordinate_System.RenderSingle(Convert.ToInt32(Math.Round(x)), (int)y, size, ConsoleColor.Cyan);
                        Thread.Sleep(1);
                        //gleichung simulieren
                        CMD_Line.Input.ClearInputField(size);
                        Formating.ConsoleWriter.LineColor($"Parabel: [{y}={stretch}*({Math.Round(x)}-{positionX})²+{positionY}]", ConsoleColor.Cyan);
                    }
                }

                //Scheitelpunkt Magenta markieren
                if (Math.Round(x, 0) == positionX - 1 && scheitelPunkt)
                {
                    UI.Coordinate_System.RenderSingle(Convert.ToInt32(Math.Round(x)), (int)y, size, ConsoleColor.Magenta); scheitelPunkt = false;
                }
            }
        }

    }
}
