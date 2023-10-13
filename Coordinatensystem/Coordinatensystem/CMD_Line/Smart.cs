using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coordinatensystem.CMD_Line
{
    internal class Smart
    {
        public static void AutoDraw(string input,int size, List<(int, int)> mainList, List<(int, int)> lastList)
        {
            char[] punkt = new char[2];
            punkt[0] = '*';
            punkt[1] = '/';
            char[] strich = new char[2];
            strich[0] = '+';
            strich[1] = '-';
            char[] satzzeichen = new char[4];
            satzzeichen[0] = '+';
            satzzeichen[1] = '-';
            satzzeichen[2] = '*';
            satzzeichen[3] = '/';
            char[] numbers = new char[10];
            numbers[0] = '0';
            numbers[1] = '1';
            numbers[2] = '2';
            numbers[3] = '3';
            numbers[4] = '4';
            numbers[5] = '5';
            numbers[6] = '6';
            numbers[7] = '7';
            numbers[8] = '8';
            numbers[9] = '9';

            // Ersetze 
            input = input.Replace("×", "*");
            input = input.Replace(",", ".");

            //Errorcheck 1
            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0 && punkt.Contains(input[i]) && satzzeichen.Contains(input[i - 1]))
                {
                    Error($"Error: Satzzeichenfehler >>{input.Substring(i - 1, i)}<<. Press any Button to continue!", size);
                }
            }

            //Missing "*" fix
            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0 && input[i]=='x' && numbers.Contains(input[i - 1]))
                {
                    input=PlaceInString(input,"*",i);
                }
                CMD_Line.Input.ClearInputField(size);
                Console.WriteLine(input);
            }

            for (float x = 0; x < size * 3 - 1; x += 1f/(size/4))
            {
                // Verwenden Sie DataTable.Compute, um den Ausdruck auszuwerten
                string input2 = input.Replace("x", Convert.ToString(x));
                input2 = input2.Replace(",", ".");
                DataTable table = new DataTable();
                table.Columns.Add("input", typeof(string), input2.Substring(2, input2.Length - 2));
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                double y = double.Parse((string)row["input"]);

                if (y < size && y >= 0)
                {
                    if (!mainList.Contains((Convert.ToInt32(Math.Round(x)), (int)y)))
                    {
                        mainList.Add((Convert.ToInt32(Math.Round(x)), (int)(y)));
                        lastList.Add((Convert.ToInt32(Math.Round(x)), (int)(y)));
                        UI.Coordinate_System.RenderSingle(Convert.ToInt32(Math.Round(x)), (int)y, size, ConsoleColor.Cyan);
                    }
                }
                CMD_Line.Input.ClearInputField(size);
                Console.WriteLine($"{y}={x}");
            }
        }
        private static void Error(string text, int size)
        {
            CMD_Line.Input.ClearInputField(size);
            Formating.ConsoleWriter.Color(text,ConsoleColor.Red);
            Console.Read();
        }
        private static string PlaceInString(string input, string characterToInsert, int positionToInsert)
        {
            string newString = input.Substring(0, positionToInsert) + characterToInsert + input.Substring(positionToInsert);

            return newString;

        }
    }
}
