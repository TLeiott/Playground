using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battle.UI//change here to merge
{
    public static class ConsoleWriter
    {
        public static void LineColor(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
        public static void Color(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = defaultColor;
        }
        public static void LadeBalken(int duration, int length = 10)
        {
            //Curserposition ablesen
            Console.CursorVisible = false;
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;

            //Ladebalkenränder anzeigen
            Color("[", ConsoleColor.White);
            Console.SetCursorPosition(cursorX + length + 1, cursorY);
            Color("]", ConsoleColor.White);
            Console.SetCursorPosition(cursorX + 1, cursorY);

            //Ladebalkenfortschritt anzeigen
            for (int i = 0; i < length; i++)
            {
                Thread.Sleep(duration / length);
                Color("█");
            }
            Thread.Sleep(duration / length);

            //Ladebalken löschen
            Console.SetCursorPosition(cursorX, cursorY);
            for (var i = 0; i < length + 2; ++i)
            {
                Console.Write(" ");
            }
        }
        public static void Slow(string message, int speed = 100, ConsoleColor color = ConsoleColor.White)
        {
            Console.CursorVisible = false;
            //Default Farben speichern und neue anwenden
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            //Nachicht anzeigen
            Random random = new Random();
            for (int i = 0; i < message.Length; ++i)
            {
                Console.Write(message[i]);
                if (message[i] == ' ') { Thread.Sleep(speed); }
                if (message[i] == '.') { Thread.Sleep(speed * 2); }
                Thread.Sleep((speed / 2) + random.Next(0, 150));
            }
            //Default farben wieder anwenden
            Console.ForegroundColor = defaultColor;
        }
    }
}