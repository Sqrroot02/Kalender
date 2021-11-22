using Kalender.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.View.Sections.CalenderViews.ViewModels
{
    public class DayView_VM : ViewModelBase
    {
        public DayView_VM()
        {

        }

        private int _day = DateTime.Today.Day;
        public int Day { get => _day; set { _day = value; NotifyPropertyChanged(nameof(Day)); } }

        private int _year = DateTime.Today.Year;
        public int Year { get => _year; set { _year = value; NotifyPropertyChanged(nameof(Year)); } }

        private int _month = DateTime.Today.Month;
        public int Month { get => _month; set { _month = value; NotifyPropertyChanged(nameof(Month)); } }
    }
}
