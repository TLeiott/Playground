using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinatensystem.CMD_Line
{
    internal class Sinus
    {
        public static void DisplaySinus(List<(int, int)> mainList, int size, string[] latest, List<(int, int)> lastList)
        {
            lastList.Clear();
            UI.Coordinate_System.Render(mainList, size);
            int positionX = 0;
            int positionY = 0;
            float stretchX = 0;
            float stretchY = 0;

            //X Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Sinus: ");
            Formating.ConsoleWriter.Color("[y = a*sin(b*x + ", ConsoleColor.Blue);//y = a sin (b*x + c) + d
            Formating.ConsoleWriter.Color("c", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color(") + d]", ConsoleColor.Blue);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die X-Coordinate der Sinuskurve ein(0-{size * 3}):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[1]; } else if (latest[1] != "") { latest[1] = input; }
                if (int.TryParse(input, out positionX) && positionX <= size) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //Y Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Sinus: ");
            Formating.ConsoleWriter.Color($"[y = a*sin(b*x + {positionX}) + ", ConsoleColor.Blue);//y = a sin (b*x + c) + d
            Formating.ConsoleWriter.Color("d", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color("]", ConsoleColor.Blue);
            Formating.ConsoleWriter.LineColor($" Bitte gieb die Y-Coordinate der Sinuskurve ein(0-{size}):");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[2]; } else if (latest[2] != "") { latest[2] = input; }
                if (int.TryParse(input, out positionY) && positionY <= size) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //Strecken x
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Sinus: ");
            Formating.ConsoleWriter.Color($"[y = a*sin(", ConsoleColor.Blue);//y = a sin (b*x + c) + d
            Formating.ConsoleWriter.Color("b", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color($"*x + {positionX}) + {positionY}", ConsoleColor.Blue);
            Formating.ConsoleWriter.LineColor($"  Bitte gieb den X-Streckungsfaktor der Sinuskurve ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[3]; } else if (latest[3] != "") { latest[3] = input; }
                if (input.Contains(",")) { input = input.Replace(",", "."); }
                if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out stretchX)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Zahl ein:", ConsoleColor.Red);
            }

            //Strecken Y
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color("Sinus: ");
            Formating.ConsoleWriter.Color($"[y = ", ConsoleColor.Blue);//y = a sin (b*x + c) + d
            Formating.ConsoleWriter.Color("d", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color($"*sin({stretchX}*x + {positionX}) + {positionY}", ConsoleColor.Blue);
            Formating.ConsoleWriter.LineColor($"  Bitte gieb den Y-Streckungsfaktor der Sinuskurve ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[4]; } else if (latest[4] != "") { latest[4] = input; }
                if (input.Contains(",")) { input = input.Replace(",", "."); }
                if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out stretchY)) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Zahl ein:", ConsoleColor.Red);
            }

            //erstellen der Sinsukurve
            for (float x = 0; x < size * 3 - 1; x += (1f / size / 2))
            {
                int y = (int)(stretchY * Math.Sin(stretchX * x + positionX - 1) + positionY - 1);
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
