using Hmd.Core.UI.Controls.CustomControls;
using Serientermine.Series;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Serientermine.ViewModels;

namespace Serientermine.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : HmdWindow
    {
        private List<Termin> termine = new List<Termin>();
        public class Termin
        {
            public int Number { get; set; }
            public DateTime Date { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            termine.Add(new Termin { Number = 1, Date = DateTime.Now });

        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Überprüfen, ob die eingegebene Zeichenfolge eine Zahl ist
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Verhindert die Eingabe des Zeichens
            }
        }

        private bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }
    }
}
