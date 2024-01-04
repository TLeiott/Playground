using Hmd.Core.UI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Hmd.Core.UI.Dialogs;
using Hmd.Core.UI.ViewModels;
using Serientermine.Series;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Serientermine.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private string _selectedSerieType;
        private int _inputDayOfMonth;
        private DateTime? _rangeStart;
        private DateTime? _rangeEnd;
        private List<DateTime> _calculatedDates;
        private int _intervall;
        private int _limit;
        private int _month;
        private int _monthDay;
        private string _weekday;


        public MainViewModel() : base(null)
        {
            RangeStart = new DateTime(2021, 1, 1);
            RangeEnd = new DateTime(2021, 1, 31);
            CalcCommand = new DelegateCommandAsync(CalculateAsync);
        }

        public ICommand CalcCommand { get; }
        public string SelectedSerieType
        {
            get => _selectedSerieType;
            set
            {
                if (!SetProperty(ref _selectedSerieType, value))
                    return;

                if (value == "Monatlich" || value == "Jährlich" || value == "Wöchentlich")
                {
                    IsWeekdayEnabled = true;
                }
                else
                {
                    IsWeekdayEnabled = false;
                }
                if (value == "Täglich" || value == "Wöchentlich")
                {
                    IsDayOfMonthEnabled = false;
                    IsSliderEnabled = false;
                }
                else
                {
                    IsDayOfMonthEnabled = true;
                    IsSliderEnabled = true;
                }

                NotifyPropertyChanged(nameof(IsWeekdayEnabled));
                NotifyPropertyChanged(nameof(IsDayOfMonthEnabled));
                NotifyPropertyChanged(nameof(IsSliderEnabled));
            }
        }
        public int InputDayOfMonth
        {
            get => _inputDayOfMonth;
            set
            {
                if (!SetProperty(ref _inputDayOfMonth, value))
                    return;
                if (value > 5)
                {
                    IsWeekdayEnabled = true;
                }
                else
                {
                    IsWeekdayEnabled = false;
                }
            }
        }

        public bool IsWeekdayEnabled { get; private set; }

        public bool IsDayOfMonthEnabled { get; private set; }
        public bool IsSliderEnabled { get; private set; }

        public DateTime? RangeStart
        {
            get => _rangeStart;
            set => SetProperty(ref _rangeStart, value);
        }
        public int Intervall
        {
            get => _intervall;
            set => SetProperty(ref _intervall, value);
        }
        public int MonthDay
        {
            get => _monthDay;
            set => SetProperty(ref _monthDay, value);
        }
        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }
        public int Limit
        {
            get => _limit;
            set => SetProperty(ref _limit, value);
        }
        public string WeekDay
        {
            get => _weekday;
            set => SetProperty(ref _weekday, ConvertToEnglish(value));
        }
        private string ConvertToEnglish(string value)
        {
            string finalResult="";
            switch (value)
            {
                case "Montag": finalResult = "Monday"; break;
                case "Dienstag": finalResult = "Tuesday"; break;
                case "Mittwoch": finalResult = "Wednesday"; break;
                case "Donnerstag": finalResult = "Thursday"; break;
                case "Freitag": finalResult = "Friday"; break;
                case "Samstag": finalResult = "Saturday"; break;
                case "Sonntag": finalResult = "Sunday"; break;
            }
            if (finalResult != "")
            {
                return finalResult;
            }
            else
            {
                return value;
            }
        }

        public DateTime? RangeEnd
        {
            get => _rangeEnd;
            set => SetProperty(ref _rangeEnd, value);
        }

        public List<DateTime> CalculatedDates
        {
            get => _calculatedDates;
            set => SetProperty(ref _calculatedDates, value);
        }

        private Task CalculateAsync(CancellationToken token)
        {
            // Validierung
            if (RangeStart == null || RangeEnd == null)
            {
                DialogService.ShowDialogHmdMessageBox(this, "Bitte Start- und Enddatum angeben.", "Fehler", HmdDialogIcon.Error);
                return Task.CompletedTask;
            }

            if (RangeStart > RangeEnd)
            {
                DialogService.ShowDialogHmdMessageBox(this, "Startdatum muss vor Enddatum liegen.", "Fehler", HmdDialogIcon.Error);
                return Task.CompletedTask;
            }

            if (Intervall < 1)
            {
                DialogService.ShowDialogHmdMessageBox(this, "Intervall muss >1 sein.", "Fehler", HmdDialogIcon.Error);
                return Task.CompletedTask;
            }

            try
            {
                IsBusy = true;

                var serie = GetSerie();

                if (serie == null)
                {
                    DialogService.ShowDialogHmdMessageBox(this, "Bitte Serientyp auswählen.", "Fehler", HmdDialogIcon.Error);
                    return Task.CompletedTask;
                }

                CalculatedDates = serie.GetDatesInRange(RangeStart.Value, RangeEnd.Value).ToList();
                //SerieBase serie;
                //serie.Name = "";
                //serie.Intervall = Int32.Parse(numberTextBoxIntervall.Text);
                //serie.Limit = Int32.Parse(numberTextBoxLimit.Text);
                //serie.Begin = MainViewModel.Start;
                //serie.End = child.GetDateTime("end");
                //serie.MonthDay = child.GetValue<int>("TagImMonat");
                //serie.Month = child.GetValue<int>("MonatImJahr");
                //var writer = new ConsoleWriter();   // Anhand von Parameter kann hier eine alternative Implementierung gewählt werden, z.B. FileWriter

                //// Jetzt enthält seriesList Ihre Liste von Serie-Objekten
                //foreach (var serie in series)
                //{
                //    writer.Write(serie, rangeStart, rangeEnd, rangeStart, rangeEnd);
                //}

                DialogService.ShowDialogHmdMessageBox(this, "Werte wurden berechnet.", "Berechnung",
                    HmdDialogIcon.Information);
            }
            catch (Exception e)
            {
                DialogService.ShowDialogHmdMessageBox(this, e.Message, "Fehler", HmdDialogIcon.Error);
            }
            finally
            {
                IsBusy = false;
            }

            return Task.CompletedTask;
        }

        private SerieBase GetSerie()
        {
            List<string> weekDayList = new List<string>();
            if (WeekDay != null && WeekDay != "")
            {
                weekDayList.Add(WeekDay);
            }
            SerieBase serie;
            switch (SelectedSerieType)
            {
                case "Täglich":
                    serie = new DailySerie();
                    break;
                case "Wöchentlich":
                    serie = new WeeklySerie
                    {
                        DayList = weekDayList
                    };
                    break;
                case "Monatlich":
                    serie = new MonthlySerie
                    {
                        DayList = weekDayList
                    };
                    break;
                case "Jährlich":
                    serie = new YearlySerie
                    {
                        DayList = weekDayList
                    };
                    break;
                default:
                    return null;
            }
            serie.Begin = (DateTime)RangeStart;
            serie.End = (DateTime)RangeEnd;
            serie.Intervall = Intervall;
            serie.Limit = Limit;
            serie.Month = Month;
            serie.MonthDay = MonthDay;


            return serie;

        }
    }
}
