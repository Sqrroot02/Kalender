using Kalender.Base;
using Kalender.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.View.Sections.CalenderViews.ViewModels
{
    /// <summary>
    /// ViewModel für die Tagesansicht 
    /// </summary>
    public class DayView_VM : ViewModelBase
    {
        private readonly ValueCheck _valueCheck = new ValueCheck();

        private int _day = CalendarData.SelectedDate.Day;
        /// <summary>
        /// Der zu Anzeigende Tag
        /// </summary>
        public int Day { get => _day; set { _day = value; NotifyPropertyChanged(nameof(Day)); } }

        private int _year = CalendarData.SelectedDate.Year;
        /// <summary>
        /// Das zu Anzeigende Jahr
        /// </summary>
        public int Year { get => _year; set { _year = value; NotifyPropertyChanged(nameof(Year)); NotifyPropertyChanged(nameof(DaysInYear)); } }

        private int _month = CalendarData.SelectedDate.Month;   
        /// <summary>
        /// Den zu Anzeigenden Monat
        /// </summary>
        public int Month { get => _month; set { _month = value; NotifyPropertyChanged(nameof(Month)); NotifyPropertyChanged(nameof(DaysInYear)); } }

        public Dictionary<int, string> Months { get => _valueCheck.Months; }
        public ObservableCollection<int> DaysInYear { get => _valueCheck.DaysInMonth(Year, Month); }
        public CommandBase<object> NextDayCom { get; set; }
        public CommandBase<object> PrevDayCom { get; set; }

        public override void InitCommand()
        {
            NextDayCom = new CommandBase<object>(NextDay);
            PrevDayCom = new CommandBase<object>(PrevDay);
        }

        public void NextDay(object obj)
        {
            if (Day == DateTime.DaysInMonth(Year, Month))
            {
                if (Month == 12)
                { 
                    Year++; 
                    Month = 1; 
                    Day = 1; 
                }
                else
                { 
                    Month++; 
                    Day = 1; 
                }
            }
            else
                Day++;
        }
        public void PrevDay(object obj)
        {
            if (Day == 1)
            {
                if (Month == 1)
                {
                    Year--; 
                    Month = 12; 
                    Day = DateTime.DaysInMonth(Year, Month);
                }
                else
                {
                    Month--; 
                    Day = DateTime.DaysInMonth(Year, Month);
                }
            }
            else
                Day--;
            
        }

    }
}
