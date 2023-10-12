using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coordinatensystem.CMD_Line
{
    internal class Vector
    {
        public static void DisplayVector(List<(int, int)> mainList, int size, string[] latest, List<(int, int)> lastList)
        {
            UI.Coordinate_System.Render(mainList, size);
            lastList.Clear();
            int x1;
            int y1;
            int x2;
            int y2;
            //X1 Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.LineColor($"Vector: Bitte gib die X-Coordinate des Vector-Startpunktes ein(0-{size * 3}):", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[1]; } else if (latest[1] != "") { latest[1] = input; }
                if (int.TryParse(input, out x1) && x1 <= size * 3) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine positive Ganzzahl bis {size * 3} ein:", ConsoleColor.Red);
            }

            //Y1 Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.LineColor($"Vector: Bitte gib die Y-Coordinate des Vector-Startpunktes ein(0-{size}):", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[2]; } else if (latest[2] != "") { latest[2] = input; }
                if (int.TryParse(input, out y1) && y1 <= size) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }
            x1--;
            y1--;
            //Spot preview erzeugen
            UI.Coordinate_System.RenderSingle(x1, y1, size, ConsoleColor.Red);

            //X2 Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.LineColor($"Vector: Bitte gieb die X-Coordinate des Vector-Ziels ein(0-{size * 3}):", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[3]; } else if (latest[3] != "") { latest[3] = input; }
                if (int.TryParse(input, out x2) && x2 <= size * 3) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size * 3} ein:", ConsoleColor.Red);
            }

            //Y2 Coordinate-Input
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.LineColor($"Vector: Bitte gieb die Y-Coordinate des Vector-Ziels ein(0-{size}):", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "") { input = latest[4]; } else if (latest[4] != "") { latest[4] = input; }
                if (int.TryParse(input, out y2) && y2 <= size) { break; }
                CMD_Line.Input.ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }
            x2--;
            y2--;
            //Spot preview erzeugen
            UI.Coordinate_System.RenderSingle(x2, y2, size, ConsoleColor.Green);
            Thread.Sleep(1000);

            //vector brechenen
            float vectorX = (x2 - x1) / ((float)size * 3);
            float vectorY = (y2 - y1) / ((float)size * 3);

            //write the vector
            for (int i = 0; i <= size * 3; i++)
            {

                if (!mainList.Contains(((int)(x1 + vectorX * i), (int)(y1 + vectorY * i))))
                {
                    mainList.Add(((int)(x1 + vectorX * i), (int)(y1 + vectorY * i)));
                    lastList.Add(((int)(x1 + vectorX * i), (int)(y1 + vectorY * i)));
                }
                UI.Coordinate_System.RenderSingle((int)(x1 + vectorX * i), (int)(y1 + vectorY * i), size, ConsoleColor.Yellow);

            }
            //ZielPoint seperat hinzufügen
            if (!mainList.Contains(((int)(x2), (int)(y2)))) { mainList.Add(((int)(x2), (int)(y2))); }
            UI.Coordinate_System.RenderSingle((int)(x2), (int)(y2), size, ConsoleColor.Yellow);
        }
    }
}
