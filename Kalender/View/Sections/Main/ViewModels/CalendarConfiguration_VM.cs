using Kalender.Base;
using Kalender.Data;
using Kalender.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.View.Sections.Main.ViewModels
{
    public class CalendarConfiguration_VM : ViewModelBase
    {
        private readonly CalendarData _calenderData = new CalendarData();

        public CalendarConfiguration_VM()
        {
            CalendarData.Calendars = new ObservableCollection<Calendar>(_calenderData.GetItems());
        }

        public override void InitCommand()
        {
            NewCalenderCommand = new CommandBase<object>(AddNewCalender);
        }

        public CommandBase<object> NewCalenderCommand { get; set; }

        public ObservableCollection<Calendar> Calendars => CalendarData.Calendars;

        public Calendar SelectedCalender { get => CalendarData.SelectedCalender; set => CalendarData.SelectedCalender = value; }

        public void AddNewCalender(object obj)
        {
            Calendar calendar = new Calendar(); 
            CalendarData.SelectedCalender = calendar;
            CalendarData.Calendars.Add(calendar);
            _calenderData.InsertItem(calendar); 
            
        }
    }
}
