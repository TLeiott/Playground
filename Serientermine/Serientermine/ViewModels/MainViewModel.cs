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

namespace Serientermine.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private string _selectedSerieType;
        private DateTime? _rangeStart;
        private DateTime? _rangeEnd;
        private List<DateTime> _calculatedDates;

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

                IsWeekdayEnabled = value == "Monatlich";

                NotifyPropertyChanged(nameof(IsWeekdayEnabled));
            }
        }

        public bool IsWeekdayEnabled { get; private set; }

        public DateTime? RangeStart
        {
            get => _rangeStart;
            set => SetProperty(ref _rangeStart, value);
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
            return new FakeSerie();

            SerieBase serie;
            switch (SelectedSerieType)
            {
                case "Täglich":
                    serie = new DailySerie();
                    break;
                case "Wöchentlich":
                    serie = new WeeklySerie();
                    break;
                case "Monatlich":
                    serie = new MonthlySerie();
                    break;
                case "Jährlich":
                    serie = new YearlySerie();
                    break;
                default:
                    return null;
            }

            return serie;

        }
    }
}
