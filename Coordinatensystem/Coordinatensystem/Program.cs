using System;
using System.Collections.Generic;

namespace Coordinatensystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                StartConsole();
                return;
            }

            if (args.Length % 2 != 0)
                throw new ArgumentException("Kommandozeilenargumente müssen paarweise angegeben werden.");

            // Mit Parametern
            var mode = 0;

            for (var i = 0; i < args.Length - 2; i += 2)
            {

                if (args[i] == "/mode")
                {
                    int.TryParse(args[i + 1], out mode);
                }
            }

            // Eingelesen Parameter auf Gültigkeit prüfen
            if (mode == 0)
            {
                Console.WriteLine("Es wurde der Parameter /mode nicht angegeben oder der Wert ist nicht größer als 0");
                return;
            }
            else if (mode == 1)
            {
                // Ausführen
            }


        }

        static void StartConsole()
        {
            //Window Resize
            int windowHeight = 3;
            int windowWidth = 110;
            string[] latest = new string[10];
            bool run = true;
            bool messageOverwrite = false;
            Console.WindowHeight = windowHeight;
            Console.WindowWidth = windowWidth;
            Console.SetWindowPosition(0, 0);

            int result;
            //Größe des Coordinatensystems
            while (true)
            {
                Console.WriteLine("Gieb die Gewünschte Größe des Coordinatensystems ein(1440p screen:max=75,1080p screen:max=60):");
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
            Console.WindowWidth = result * 4;

            //Create Background
            UI.Coordinate_System.Background(size);

            //Create List
            List<(int, int)> mainList = new List<(int, int)>();
            List<(int, int)> lastList = new List<(int, int)>();

            //input
            CMD_Line.Input.MainCMD(mainList, lastList,size);
            Console.Clear();
            Formating.ConsoleWriter.Slow("Vielen Dank für das nutzen meines kleinen Programmes. Ich hoffe es hat dir gefallen. Für das schließen dieses Fensters drücke eine Beliebige Taste. :)");
            Console.WriteLine();
            Formating.ConsoleWriter.Slow(" <3 ", 25, ConsoleColor.Magenta);
            Console.Read();

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