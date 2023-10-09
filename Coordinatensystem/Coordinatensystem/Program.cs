using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coordinatensystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Window Resize
            int windowHeight = 3;
            int windowWidth = 70;
            Console.WindowHeight = windowHeight;
            Console.WindowWidth = windowWidth;
            Console.SetWindowPosition(0, 0);

            int result;
            //Größe des Coordinatensystems
            while (true)
            {
                Console.WriteLine("Gieb die Gewünschte Größe des Coordinatensystems ein(5-75):");
                string input = Console.ReadLine();
                if (input == "") { result = 75; break; }
                else if (int.TryParse(input, out result) && result <= 75 && result >= 5) { break; }
                Console.Clear();
                Formating.ConsoleWriter.LineColor("Error. Bitte gieb eine Ganzzahl im Bereich zwischen 5 und 75 ein!", ConsoleColor.Red);
            }
            Console.Clear();
            Formating.ConsoleWriter.LadeBalken(result * 1);
            int size = result;

            //Window Resize 
            Console.WindowHeight = result + 4;
            Console.WindowWidth = result * 3;

            //Create Background
            Background(size);

            //Create List
            List<(int, int)> mainList = new List<(int, int)>();


            //input
            bool run = true;
            bool messageOverwrite = false;
            while (run)
            {
                ClearInputField(size, messageOverwrite);
                Console.SetCursorPosition(0, size + 1);
                Console.SetWindowPosition(0, 0);

                //startmessage overwrite
                if (messageOverwrite) { messageOverwrite = false; }
                else { Formating.ConsoleWriter.LineColor("Please input a Command(h for help):"); Console.CursorVisible = true; }

                Console.SetCursorPosition(0, size + 2);
                string input = Console.ReadLine();
                switch (input)
                {
                    case "h":
                    case "H"://Help
                        messageOverwrite = DisplayHelpInformation(size);
                        break;
                    case "p":
                    case "P"://Parabel
                        mainList = DisplayParabel(mainList, size); break;
                    case "c":
                    case "C"://Clear
                        mainList = ClearList(mainList, size); messageOverwrite = true; break;
                    case "v":
                    case "V"://Vector
                        messageOverwrite = true;
                        DisplayVector(mainList, size);
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
                    default:
                        Render(mainList, size); break;
                }
            }
            Console.Clear();
            Formating.ConsoleWriter.Slow("Vielen Dank für das nutzen meines kleinen Programmes. Ich hoffe es hat dir gefallen. Für das schließen dieses Fensters drücke eine Beliebige Taste. :)");
            Console.WriteLine();
            Formating.ConsoleWriter.Slow(" <3 ", 25, ConsoleColor.Magenta);
            Console.Read();
        }
        static void Background(int size)
        {
            for (int i = 0; i < size; i++)
            {
                // for animation
                Thread.Sleep(0);

                //anzeigen
                var s = string.Empty.PadLeft(size, '░');
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
            for (int i = 0; i <= size; i++)
            {
                if (i <= 4) { Console.Write(i); }
                else if (i % 5 == 0 && i % 10 != 0) { Formating.ConsoleWriter.Color("5", ConsoleColor.White); }
                else if (i % 10 == 0) { Formating.ConsoleWriter.Color("█", ConsoleColor.White); }
                else { Formating.ConsoleWriter.Color("═", ConsoleColor.Gray); }
            }
        }
        static void Render(List<(int, int)> mainList, int size, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            foreach (var value in mainList)
            {
                Console.SetCursorPosition(value.Item1 + 1, -value.Item2 + size - 1);
                Formating.ConsoleWriter.LineColor("█", color);
            }
        }
        static void RenderSingle(int x, int y, int size, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x + 1, -y + size - 1);
            Formating.ConsoleWriter.LineColor("█", color);
            Thread.Sleep(1);
        }
        static void ClearInputField(int size, bool messageOverwrite = false)
        {
            Console.SetCursorPosition(0, size + 1);

            //skip messagefield
            if (!messageOverwrite) { Console.WriteLine(string.Empty.PadLeft(150, ' ')); } else { Console.WriteLine(""); }

            Console.WriteLine(string.Empty.PadLeft(150, ' '));
            Console.SetCursorPosition(0, size + 1);
        }
        static List<(int, int)> ClearList(List<(int, int)> mainList, int size)
        {
            Background(size);
            mainList.Clear();
            ClearInputField(size, false);
            Formating.ConsoleWriter.LineColor("List cleared succesfully. Write your next command:", ConsoleColor.Green);
            return mainList;
        }
        static bool DisplayHelpInformation(int size)
        {
            Console.SetCursorPosition(0, size + 1);
            Formating.ConsoleWriter.LineColor("Help: (h/H=Help,p/P=Parabel,v/V=Vector,q/Q=Quit) Please write ONE Command:", ConsoleColor.Green);
            return true;
        }
        static List<(int, int)> DisplayParabel(List<(int, int)> mainList, int size)
        {
            Render(mainList, size);
            int positionX;
            int positionY;
            decimal stretch;
            //X Coordinate-Input
            ClearInputField(size);
            Formating.ConsoleWriter.Color("Parabel: ");
            Formating.ConsoleWriter.Color("[y=a*(x-", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color("b", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color(")²+c]", ConsoleColor.Cyan);
            Formating.ConsoleWriter.LineColor(" Bitte gieb die X-Coordinate des Scheitelpunkts ein:");
            ///WriteLineColor("Parabel: [y=a*(x-b)²+c] Bitte gieb die X-Coordinate des Scheitelpunkts ein:", ConsoleColor.Cyan);
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out positionX) && positionX <= size) { break; }
                ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //Y Coordinate-Input
            ClearInputField(size);
            Formating.ConsoleWriter.Color("Parabel: ");
            Formating.ConsoleWriter.Color($"[y=a*(x-{positionX})²+", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color("c", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color("]", ConsoleColor.Cyan);
            Formating.ConsoleWriter.LineColor(" Bitte gieb die Y-Coordinate des Scheitelpunkts ein:");
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out positionY) && positionY <= size) { break; }
                ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //Stauchen oder Strecken
            ClearInputField(size);
            Formating.ConsoleWriter.Color("Parabel: ");
            Formating.ConsoleWriter.Color("[y=", ConsoleColor.Cyan);
            Formating.ConsoleWriter.Color("a", ConsoleColor.Magenta);
            Formating.ConsoleWriter.Color($"*(x-{positionX})²+{positionY}]", ConsoleColor.Cyan);
            Formating.ConsoleWriter.LineColor("  Soll die Parabel gestaucht(0.XX) oder gestreckt(XX.0) sein?:");
            while (true)
            {
                string input = Console.ReadLine();
                if (decimal.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out stretch)) { break; }
                ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Zahl ein:", ConsoleColor.Red);
            }

            //erstellen der Parabel
            bool scheitelPunkt = true;
            for (float x = 0; x < size - 1; x += (1f / size))
            {
                double power = Math.Pow(x, 2);
                int y = (int)(stretch * (decimal)(Math.Pow((x - (positionX - 1)), 2)) + (positionY - 1M));
                if (y < size && y >= 0)
                {
                    if (!mainList.Contains((Convert.ToInt32(Math.Round(x)), (int)y)))
                    {
                        mainList.Add((Convert.ToInt32(Math.Round(x)), (int)y));
                        RenderSingle(Convert.ToInt32(Math.Round(x)), (int)y, size, ConsoleColor.Cyan);
                        Thread.Sleep(1);
                        //gleichung simulieren
                        ClearInputField(size);
                        Formating.ConsoleWriter.LineColor($"Parabel: [{y}={stretch}*({Math.Round(x)}-{positionX})²+{positionY}]", ConsoleColor.Cyan);
                    }
                }

                //Scheitelpunkt Magenta markieren
                if (Math.Round(x,0) == positionX-1&& scheitelPunkt)
                {
                    RenderSingle(Convert.ToInt32(Math.Round(x)), (int)y, size, ConsoleColor.Magenta); scheitelPunkt=false;
                }
            }
            return mainList;
        }
        static List<(int, int)> DisplayVector(List<(int, int)> mainList, int size)
        {
            Render(mainList, size);
            int x1;
            int y1;
            int x2;
            int y2;
            //X1 Coordinate-Input
            ClearInputField(size);
            Formating.ConsoleWriter.LineColor("Vector: Bitte gib die X-Coordinate des Vector-Startpunktes ein:", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out x1) && x1 <= size) { break; }
                ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //Y1 Coordinate-Input
            ClearInputField(size);
            Formating.ConsoleWriter.LineColor("Vector: Bitte gib die Y-Coordinate des Vector-Startpunktes ein:", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out y1) && y1 <= size) { break; }
                ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gib eine positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }
            x1--;
            y1--;
            //Spot preview erzeugen
            RenderSingle(x1, y1, size, ConsoleColor.Red);

            //X2 Coordinate-Input
            ClearInputField(size);
            Formating.ConsoleWriter.LineColor("Vector: Bitte gieb die X-Coordinate des Vector-Ziels ein:", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out x2) && x2 <= size) { break; }
                ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }

            //Y2 Coordinate-Input
            ClearInputField(size);
            Formating.ConsoleWriter.LineColor("Vector: Bitte gieb die Y-Coordinate des Vector-Ziels ein:", ConsoleColor.Yellow);
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out y2) && y2 <= size) { break; }
                ClearInputField(size);
                Formating.ConsoleWriter.LineColor($"Fehler! Bitte gieb eine Positive Ganzzahl bis {size} ein:", ConsoleColor.Red);
            }
            x2--;
            y2--;
            //Spot preview erzeugen
            RenderSingle(x2, y2, size, ConsoleColor.Green);
            Thread.Sleep(1000);

            //vector brechenen
            float vectorX = (x2 - x1) / (float)size;
            float vectorY = (y2 - y1) / (float)size;

            //write the vector
            for (int i = 0; i <= size; i++)
            {

                if (!mainList.Contains(((int)(x1 + vectorX * i), (int)(y1 + vectorY * i)))) { mainList.Add(((int)(x1 + vectorX * i), (int)(y1 + vectorY * i))); }
                RenderSingle((int)(x1 + vectorX * i), (int)(y1 + vectorY * i), size, ConsoleColor.Yellow);

            }
            //ZielPoint seperat hinzufügen
            if (!mainList.Contains(((int)(x2), (int)(y2)))) { mainList.Add(((int)(x2), (int)(y2))); }
            RenderSingle((int)(x2), (int)(y2), size, ConsoleColor.Yellow);
            return mainList;
        }
        /*  Hilfe von Ronny 
        static void DoIt()
        {
            List<(int, int)> values = new List<(int, int)>();

            values.Add((1, 2));

            foreach (var value in values)
            {
                Console.SetCursorPosition(value.Item1, value.Item2);
                Console.WriteLine("█");
            }
        }*/
    }
}