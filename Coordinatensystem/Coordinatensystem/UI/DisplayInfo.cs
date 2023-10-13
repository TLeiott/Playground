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
            Formating.ConsoleWriter.LineColor("Help: (h/H=Help, p/P=Parabel, v/V=Vector, s/S=SinusCurve, c/C=Cirlce, z/Z=Undo the last Step, q/Q=Quit,clear/Clear=clear the field, noInput=repeat last input step by step, y= ... Smart AutoDraw [BETA]) Please write ONE Command:", ConsoleColor.Green);
            return true;
        }
    }
}
