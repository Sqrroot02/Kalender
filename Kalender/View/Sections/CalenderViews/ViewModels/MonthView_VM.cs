using Kalender.Base;
using Kalender.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.View.Sections.CalenderViews.ViewModels
{
    public class MonthView_VM : ViewModelBase
    {
        public ValueCheck _valueCheck = new ValueCheck();

        public MonthView_VM()
        {
            InitCommand();
        }

        private int _year = CalendarData.SelectedDate.Year;
        public int Year { get => _year; set { _year = value; NotifyPropertyChanged(nameof(Year)); NotifyPropertyChanged(nameof(ShowedDate)); } }

        private int _month = CalendarData.SelectedDate.Month;
        public int Month { get => _month; set { _month = value; NotifyPropertyChanged(nameof(Month)); NotifyPropertyChanged(nameof(ShowedDate)); } }

        public DateTime ShowedDate 
        {   
            get { try { return new DateTime(Year, Month, 1); } catch { return DateTime.Now; } }
        }

        public Dictionary<int, string> Months { get => _valueCheck.Months; }

        public CommandBase<object> NextMonthCom { get; set; }
        public CommandBase<object> PreviousMonthCom { get; set; }

        public override void InitCommand()
        {
            NextMonthCom = new CommandBase<object>(NextMonth);
            PreviousMonthCom = new CommandBase<object>(PreviousMonth);
        }

        public void NextMonth(object obj)
        {
            if (Month == 12)
            {
                Month = 1;
                Year++;
            }
            else
                Month++;
        }


        public void PreviousMonth(object obj)
        {
            if (Month == 1)
            {
                Month = 12;
                Year--;
            }
            else
                Month--;
        }
            
    }
}
