using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.UI
{
    internal class DisplayImage
    {
        private static ConsoleColor[] allColors = GetAllColors();

        public static ConsoleColor[] GetAllColors()
        {
            ConsoleColor[] allColors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));
            int whiteIndex = Array.IndexOf(allColors, ConsoleColor.White);
            if (whiteIndex > 0)
            {
                // Swap white with the first color
                ConsoleColor temp = allColors[0];
                allColors[0] = ConsoleColor.White;
                allColors[whiteIndex] = temp;
            }
            return allColors;
        }
        public static void General(int x, int y)
        {
            foreach (var color in allColors)
            {
                Console.SetCursorPosition(x, y);
                UI.DisplayImported.WriteFromFile($"Basic__{color}.txt", x, y);
            }
        }
        public static void Present(int x, int y)
        {
            foreach (var color in allColors)
            {
                Console.SetCursorPosition(x, y);
                UI.DisplayImported.WriteFromFile($"Present__{color}.txt", x, y);
            }
        }
    }
}
