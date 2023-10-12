using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinatensystem.UI
{
    internal class DisplayInfo
    {
        public static bool DisplayHelpInformation(int size)
        {
            Console.SetCursorPosition(0, size + 1);
            Formating.ConsoleWriter.LineColor("Help: (h/H=Help,p/P=Parabel,v/V=Vector,q/Q=Quit) Please write ONE Command:", ConsoleColor.Green);
            return true;
        }
    }
}
