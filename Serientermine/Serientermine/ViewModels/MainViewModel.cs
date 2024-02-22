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
using Serientermine.Providers;
using Serientermine.UI;
using System.Collections.ObjectModel;
using Hmd.Environments;
using Microsoft.EntityFrameworkCore;
using SerientermineErmitteln.Data.Database;

namespace Serientermine.ViewModels
{
    internal sealed class MainViewModel : WindowViewModel, IReloadableViewModel
    {
        private string _selectedSerieType;
        private int _inputDayOfMonth;
        private DateTime _serieStart;
        private DateTime? _serieEnd;
        private List<DateTime> _calculatedDates;
        private int _intervall;
        private int _limit;
        private int _month;
        private int _monthDay;
        private string _weekday;
        public DateTime _rangeStart;
        public DateTime _rangeEnd;

        private ObservableCollection<ISerie> _series;
        private ISerie _selectedSerie;
        private string _calculatedSerieName;
        private int _calculatedSerieCount;

        public MainViewModel() : base(null)
        {
            SerieStart = new DateTime(2024, 1, 1);
            SerieEnd = new DateTime(2024, 12, 31);
            RangeStart = new DateTime(0001, 1, 1);
            RangeEnd = new DateTime(9998, 12, 31);
            Month = 1;

            CreateCommand = new DelegateCommand(CreateSerie);
            EditCommand = new DelegateCommand(EditSerie, () => SelectedSerie != null);
            DeleteCommand = new DelegateCommand(DeleteSerieAsync, () => SelectedSerie != null);
            CalcCommand = new DelegateCommandAsync(CalculateAsync, () => SelectedSerie != null);
        }

        //public override Task InitializeAsync(CancellationToken token)
        //    => ReloadAsync(token);

        public async Task ReloadAsync(CancellationToken token)
        {
            try
            {
                IsBusy = true;

                //await Task.Delay(1000);
                var selected = SelectedSerie;

                var provider = HmdEnvironment.GetRequiredService<ISeriesProvider>();
                Series = new ObservableCollection<ISerie>(await provider.GetSeriesAsync(token));
                SelectedSerie = Series.FirstOrDefault(x => x.Name == selected?.Name);
            }
            catch (Exception e)
            {
                DialogService.ShowDialogHmdException(this, e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override IEnumerable<IDelegateCommand> GetNotifyRequiredCommands()
        {
            yield return EditCommand;
            yield return DeleteCommand;
            yield return CalcCommand;
        }

        public ObservableCollection<ISerie> Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        public ISerie SelectedSerie
        {
            get => _selectedSerie;
            set => SetProperty(ref _selectedSerie, value);
        }

        public string CalculatedSerieName
        {
            get => _calculatedSerieName;
            set => SetProperty(ref _calculatedSerieName, value);
        }

        public int CalculatedSerieCount
        {
            get => _calculatedSerieCount;
            set => SetProperty(ref _calculatedSerieCount, value);
        }

        public ICommand CreateCommand { get; }

        public IDelegateCommand EditCommand { get; }

        public IDelegateCommand DeleteCommand { get; }

        public IDelegateCommand CalcCommand { get; }
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
                }
                if (value == "Jährlich")
                {
                    IsSliderEnabled = true;
                }
                else
                {
                    IsSliderEnabled = false;
                }

                NotifyPropertyChanged(nameof(IsWeekdayEnabled));
                NotifyPropertyChanged(nameof(IsDayOfMonthEnabled));
                NotifyPropertyChanged(nameof(IsSliderEnabled));
            }
        }
        public bool IsWeekdayEnabled { get; private set; }
        public bool IsDayOfMonthEnabled { get; private set; }
        public bool IsSliderEnabled { get; private set; }

        public DateTime SerieStart
        {
            get => _serieStart;
            set => SetProperty(ref _serieStart, value);
        }
        public DateTime RangeStart
        {
            get => _rangeStart;
            set => SetProperty(ref _rangeStart, value);
        }
        public DateTime RangeEnd
        {
            get => _rangeEnd;
            set => SetProperty(ref _rangeEnd, value);
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
            set => SetProperty(ref _weekday, value);
        }
        public DateTime? SerieEnd
        {
            get => _serieEnd;
            set => SetProperty(ref _serieEnd, value);
        }
        public List<DateTime> CalculatedDates
        {
            get => _calculatedDates;
            set => SetProperty(ref _calculatedDates, value);
        }

        private void CreateSerie()
        {
            var serie = DialogService.ShowCreateSerieDialog(this);
            if (serie == null)
                return;

            Series.Add(serie);
            ReloadAsync(CancellationToken.None);
        }

        private void EditSerie()
        {
            if (SelectedSerie == null)
                return;

            DialogService.ShowEditSerieDialog(this, SelectedSerie);
            //tabaseSeriesProvider.DeleteSeriesAsync(SelectedSerie.Id);
            ReloadAsync(CancellationToken.None);

            // _ = ReloadAsync(CancellationToken.None);
        }

        private async void DeleteSerieAsync()
        {
            if (SelectedSerie == null)
                return;
            await DatabaseSeriesProvider.DeleteSeriesAsync(SelectedSerie.Id);
            ReloadAsync(CancellationToken.None);
            //WEITER: Hier ist das Problem, bessergasgt warscheinlich der ansatz zur lösung. Das Löschen funktioniert noch nicht. und wenn man eine Serie bearbeitet, dann wird die neue Version zwar gespeichert, aber das alte nicht gelösch oder überschrieben. 
        }

        private Task CalculateAsync(CancellationToken token)
        {

            if (SelectedSerie == null)
                return Task.CompletedTask;

            try
            {
                IsBusy = true;

                CalculatedSerieName = SelectedSerie.Name;
                CalculatedSerieCount = SelectedSerie.GetDatesInRange(_rangeStart, _rangeEnd).Count();
                CalculatedDates = SelectedSerie.GetDatesInRange(_rangeStart, _rangeEnd).ToList();

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
    }
}