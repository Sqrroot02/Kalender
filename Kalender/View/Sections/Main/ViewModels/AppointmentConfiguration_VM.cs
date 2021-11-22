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
    public class AppointmentConfiguration_VM : ViewModelBase
    {
        private readonly AppointmentData _appointmentData = new AppointmentData();
        private readonly CalendarData _calendarData = new CalendarData();   
        public AppointmentConfiguration_VM()
        {
            AppointmentData.OnSelectedAppointmentChanged += AppointmentData_OnSelectedAppointmentChanged;
            CalendarData.SelectedDateChanged += CalendarData_SelectedDateChanged;
            InitCommand();
            SelectedAppointment = new Appointment();
        }

        private void CalendarData_SelectedDateChanged(object? sender, DateTime e)
        {
            NotifyPropertyChanged(nameof(DayAppointments));
        }

        private void AppointmentData_OnSelectedAppointmentChanged(object? sender, Appointment e)
        {
            NotifyPropertyChanged(nameof(SelectedAppointment));
        }

        public override void InitCommand()
        {
            ApplyCommand = new CommandBase<object>(UpdateAppointment);
            AddCommand = new CommandBase<object>(AddNewAppointment);
            DeleteCommand = new CommandBase<object>(DeleteAppointment);
        }

        /// <summary>
        /// Der derzeitig Ausgweählte Termin
        /// </summary>
        public Appointment SelectedAppointment { get => AppointmentData.SelectedAppointment; set => AppointmentData.SelectedAppointment = value; }
        
        /// <summary>
        /// Die Termine für den Ausgewählten Tag
        /// </summary>
        public ObservableCollection<Appointment> DayAppointments
        {
            get => new ObservableCollection<Appointment>(Appointments.Where(x => x.Date.Day == CurrentDate.Day));
        }

        public CommandBase<object> DeleteCommand { get; set; }
        public CommandBase<object> ApplyCommand { get; set; }
        public CommandBase<object> AddCommand { get; set; }

        private DateTime _currentDate = DateTime.Now;
        public DateTime CurrentDate { get => CalendarData.SelectedDate; }

        public void AddNewAppointment(object obj)
        {
            Appointment appointment = new Appointment()
            {
                Title = "New Appointment",
                Date = CurrentDate,
                Description = "",
                Location = "",
                AppointmentId = Guid.NewGuid()
            };
            AppointmentData.Appointments.Add(appointment);
            _appointmentData.InsertItem(appointment);
            SelectedAppointment = appointment;
            NotifyPropertyChanged(nameof(DayAppointments));
        }

        public void UpdateAppointment(object obj)
        {
            _appointmentData.UpdateItem(SelectedAppointment);
        }

        public void DeleteAppointment(object obj)
        {
            _appointmentData.DeleteItem(SelectedAppointment);
            Appointments.Remove(SelectedAppointment);   
            SelectedAppointment=null;
        }
    }
}
