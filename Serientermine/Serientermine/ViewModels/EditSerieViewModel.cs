using Hmd.Core.UI.Commands;
using Hmd.Core.UI.Dialogs;
using Hmd.Core.UI.ViewModels;
using Hmd.Environments;
using Serientermine.Providers;
using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Serientermine.ViewModels
{
    internal sealed class EditSerieViewModel : EditViewModel
    {
        private string _selectedSerieType;
        private int _intervall = 1;
        private int _limit;
        private int _month;
        private int _monthDay;
        private string _weekday = string.Empty;
        private string _name = string.Empty;
        private ISerie _serie;
        private DateTime _serieStart = DateTime.Today;
        private DateTime? _serieEnd;

        public EditSerieViewModel(IViewModel parent) : base(parent, true)
        {
            Title = "Serie erstellen";
            SaveBehaviour = EditSaveBehaviour.ShowMessageOnSuccess;
            IsTypeEnabled = true;
            SaveCommand = new DelegateCommandAsync(SaveAsync);
            NotifyPropertyChanged(nameof(IsTypeEnabled));
        }

        public EditSerieViewModel(IViewModel parent, ISerie serie) : base(parent, false)
        {
            _serie = serie ?? throw new ArgumentNullException(nameof(serie));

            Title = "Serie bearbeiten";
            Subtitle = _serie.Name;
            Name = _serie.Name;
            SelectedSerieType = _serie.Type1.ToString();
            Intervall = _serie.Intervall;
            Limit = _serie.Limit;
            Month = _serie.Month;
            MonthDay = _serie.MonthDay;
            SerieStart = _serie.Begin;
            SerieEnd = _serie.End;
            WeekDay = _serie.WeekDay;
            //if(_serie=DailySerie)
            //WeekDay = DayOfWeek.Sunday.ToGermanText();

            //WeekDay= ConvertFromEnglish(_serie.DayList[0]);
            IsDirty = false;
            IsTypeEnabled = false;
            NotifyPropertyChanged(nameof(IsTypeEnabled));
        }

        public bool IsWeekdayEnabled { get; private set; }
        public bool IsDayOfMonthEnabled { get; private set; }
        public bool IsTypeEnabled { get; private set; }
        public bool IsSliderEnabled { get; private set; }
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
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string WeekDay
        {
            get => _weekday;
            set
            {
                if (!SetProperty(ref _weekday, value))
                    return;

                SetFieldValidationErrors(ValidateWeekDay());
            }
        }

        public string SelectedSerieType
        {
            get => _selectedSerieType;
            set
            {
                if (!SetProperty(ref _selectedSerieType, value))
                    return;

                if (value == "Monthly" || value == "Yearly" || value == "Weekly")
                {
                    IsWeekdayEnabled = true;
                }
                else
                {
                    IsWeekdayEnabled = false;
                }

                if (value == "Daily" || value == "Weekly")
                {
                    IsDayOfMonthEnabled = false;
                    IsSliderEnabled = false;
                }
                else
                {
                    IsDayOfMonthEnabled = true;
                }
                if (value == "Yearly")
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

                SetFieldValidationErrors(ValidateWeekDay(), nameof(WeekDay));
            }
        }
        public DateTime SerieStart
        {
            get => _serieStart;
            set => SetProperty(ref _serieStart, value);
        }
        public DateTime? SerieEnd
        {
            get => _serieEnd;
            set => SetProperty(ref _serieEnd, value);
        }
        public ISerie SavedSerie => _serie;

        protected override void ValidateAll()
        {
            SetFieldValidationErrors(ValidateWeekDay(), nameof(WeekDay));
            SetFieldValidationErrors(ValidateIntervall(), nameof(Intervall));
            SetFieldValidationErrors(ValidateName(), nameof(Name));
            SetFieldValidationErrors(ValidateMonthDay(), nameof(MonthDay));
        }

        private IEnumerable<string> ValidateWeekDay()
        {
            if (string.IsNullOrWhiteSpace(WeekDay) && SelectedSerieType == "Weekly")
                yield return "Es muss ein Wochentag ausgewählt werden.";
        }

        private IEnumerable<string> ValidateIntervall()
        {
            if (Intervall < 1 || Intervall.ToString() == "")
                yield return "Intervall muss >0 sein.";
        }

        private IEnumerable<string> ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return "Ein Name muss angegeben sein.";
        }
        private IEnumerable<string> ValidateMonthDay()
        {
            if (MonthDay < 1 && (SelectedSerieType == "Monthly" || SelectedSerieType == "Yearly"))
                yield return "Tag des Monats muss >0 sein";
        }

        protected override Task<(bool, string)> SavingAsync(CancellationToken token)
        {
            SerieBase serieBase = _serie as SerieBase;
            if (IsCreateMode)
            {
                switch (_selectedSerieType?.ToString())
                {
                    case "Daily":
                        serieBase = new DailySerie();
                        break;
                    case "Weekly":
                        serieBase = new WeeklySerie();
                        break;
                    case "Monthly":
                        serieBase = new MonthlySerie();
                        break;
                    case "Yearly":
                        serieBase = new YearlySerie();
                        break;
                    default:
                        throw new NotSupportedException($"Der Serientyp '{_selectedSerieType}' ist noch nicht implementiert.");
                }
            }

            //serieBase.Name = Name
            serieBase.Begin = SerieStart;
            serieBase.End = SerieEnd;
            serieBase.Intervall = Intervall;
            serieBase.Limit = Limit;
            serieBase.Month = Month;
            serieBase.MonthDay = MonthDay;
            serieBase.WeekDay = WeekDay;
            serieBase.Name = Name;

            switch (_serie)
            {
                case DailySerie daily:
                    break;
            }
            var provider = HmdEnvironment.GetRequiredService<ISeriesProvider>();
            if(IsCreateMode)
            {
                provider.CreateAsync(serieBase, token);
            }
            else
            {
                provider.UpdateAsync(serieBase, token);
            }
            InitializeAsync(token);

            return Task.FromResult((true, "Das Speichern war erfolgreich."));
        }
        /// <summary>
        /// Checkt ob mit diesen werten gespeichert werden kann.
        /// </summary>
        private bool CheckLegitimacy()
        {
            if (Intervall < 1 || Intervall.ToString() == "")
            {
                DialogService.ShowDialogHmdMessageBox(this, "Intervall muss >0 sein.", "Fehler", HmdDialogIcon.Error);
                return false;
            }
            else if (Name.Length < 1 || Name == "")
            {
                DialogService.ShowDialogHmdMessageBox(this, "Ein Name muss angegeben sein.", "Fehler", HmdDialogIcon.Error);
                return false;
            }
            else if (MonthDay < 1 && (SelectedSerieType == "Monthly" || SelectedSerieType == "Yearly"))
            {
                DialogService.ShowDialogHmdMessageBox(this, "Tag des Monats muss >0 sein", "Fehler", HmdDialogIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}