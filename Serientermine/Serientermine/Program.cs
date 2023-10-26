using Serientermine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Resize Window
            Console.WindowHeight = 60;
            Console.WindowWidth = 200;

            List<Input.Serie> serien = new List<Input.Serie>();
            serien.Add(new Serie { ID = 1, StartDatum = DateTime.Parse("01.01.2022"), EndDatum = DateTime.Parse("01.01.2023"), Wochentage= new List<int> { } });


            UI.MainRender.Render(2);
            Input.MainInput.Input();
        }
    }
}
