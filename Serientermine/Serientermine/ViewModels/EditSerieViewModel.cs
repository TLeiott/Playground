using Hmd.Core.UI.Validations.Entities;
using Hmd.Core.UI.ViewModels;
using Serientermine.Serientermine;
using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private ISerie _serie;
        private DateTime _serieStart = DateTime.Today;
        private DateTime? _serieEnd;

        public EditSerieViewModel(IViewModel parent) : base(parent, true)
        {
            Title = "Serie erstellen";
            //SaveBehaviour = EditSaveBehaviour.ShowMessageOnSuccess;
        }

        public EditSerieViewModel(IViewModel parent, ISerie serie) : base(parent, false)
        {
            _serie = serie;

            Title = "Serie bearbeiten";
            Subtitle = _serie.Name;
            SelectedSerieType = _serie.Type.ToString();
            Intervall = _serie.Intervall;
            Limit = _serie.Limit;
            Month = _serie.Month;
            MonthDay = _serie.MonthDay;
            SerieStart = _serie.Begin;
            SerieEnd = _serie.End;
            //if(_serie=DailySerie)
            //WeekDay = DayOfWeek.Sunday.ToGermanText();

            //WeekDay= ConvertFromEnglish(_serie.DayList[0]);
            IsDirty = false;

            //SaveBehaviour = EditSaveBehaviour.ShowMessageOnSuccess;
        }

        public bool IsWeekdayEnabled { get; private set; }
        public bool IsDayOfMonthEnabled { get; private set; }
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
        }

        private IEnumerable<string> ValidateWeekDay()
        {
            if (string.IsNullOrWhiteSpace(WeekDay) && SelectedSerieType == "Weekly")
                yield return "Es muss ein Wochentag ausgewählt werden.";
        }

        protected override Task<(bool, string)> SavingAsync(CancellationToken token)
        {
            if (IsCreateMode)
            {
                ISerie serie;
                switch (_serie.Type.ToString())
                {
                    case "Daily":
                        serie = new DailySerie();
                        break;
                    case "Weekly":
                        serie = new WeeklySerie
                        {
                            //DayList = GetDayList(child.GetValue<string>("Wochentage"))
                        };
                        break;
                    case "Monthly":
                        serie = new MonthlySerie
                        {
                            //DayList = GetDayList(child.GetValue<string>("Wochentage"))
                        };
                        break;
                    case "Yearly":
                        serie = new YearlySerie
                        {
                            //DayList = GetDayList(child.GetValue<string>("Wochentage"))
                        };
                        break;
                    default:
                        throw new NotSupportedException($"Der Serientyp '{_serie.Type}' ist noch nicht implementiert.");
                }
            }
            else if (_serie is SerieBase value && !value.Name.Contains(".Saved"))
            {
                value.Name += ".Saved";
            }

            return Task.FromResult((true, "Das Speichern war erfolgreich."));
        }
    }
}