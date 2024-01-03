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

            //DateGrid.ItemsSource = termine;
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

        private void NumberTextDayOfMonth_LostFocus(object sender, TextChangedEventArgs e)
        {
            //if (int.TryParse(numberTextDayOfMonth.Text, out int dayOfMonth))
            //{
            //    if (dayOfMonth > 5)
            //    {
            //        WeekDayComboBox.IsEnabled = false;
            //    }
            //    else
            //    {
            //        WeekDayComboBox.IsEnabled = true;
            //    }
            //}
            //else
            //{
            //    WeekDayComboBox.IsEnabled = true;
            //}
        }
        private void SerieTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (SerieTypeComboBox.SelectedItem != null)
            //{
            //    string selectedValue = (SerieTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            //    if (selectedValue == "Täglich" || selectedValue == "Wöchentlich")
            //    {
            //        monthSlider.IsEnabled = false;
            //    }
            //    else
            //    {
            //        monthSlider.IsEnabled = true;
            //    }
            //}
            //else
            //{
            //    monthSlider.IsEnabled = false;
            //}
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var rangeStart = new DateTime(2020, 1, 1);
            var rangeEnd = new DateTime(2021, 1, 13);

        }
    }
}
