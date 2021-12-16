using Kalender.Base;
using Kalender.Data;
using Kalender.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Kalender.View.Sections.Main.ViewModels
{
    /// <summary>
    /// ViewModel für das Termin-Bearbeitungs Panel
    /// </summary>
    public class AppointmentConfiguration_VM : ViewModelBase
    {
        #region Variablen
        private readonly AppointmentData _appointmentData = new AppointmentData();
        #endregion

        public AppointmentConfiguration_VM()
        {
            AppointmentData.OnSelectedAppointmentChanged += AppointmentData_OnSelectedAppointmentChanged;
            CalendarData.SelectedDateChanged += CalendarData_SelectedDateChanged;
            InitCommand();
        }

        #region Event Methoden
        private void CalendarData_SelectedDateChanged(object? sender, DateTime e) =>
            NotifyPropertyChanged(nameof(DayAppointments));            

        private void AppointmentData_OnSelectedAppointmentChanged(object? sender, Appointment e)        
        {
            NotifyPropertyChanged(nameof(SelectedAppointment));
            if (AppointmentData.SelectedAppointment != null)
                AppointmentOptionVisibility = Visibility.Visible;
            else
                AppointmentOptionVisibility = Visibility.Collapsed;
        }
        #endregion

        public override void InitCommand()
        {
            ApplyCommand = new CommandBase<object>(UpdateAppointment);
            AddCommand = new CommandBase<object>(AddNewAppointment);
            DeleteCommand = new CommandBase<object>(DeleteAppointment);
        }

        #region Eigenschaften
        /// <summary>
        /// Der derzeitig Ausgweählte Termin
        /// </summary>
        public Appointment SelectedAppointment { get => AppointmentData.SelectedAppointment; set => AppointmentData.SelectedAppointment = value; }
        
        private Visibility _appointmentOptionsVisibility = Visibility.Collapsed;
        public Visibility AppointmentOptionVisibility { get => _appointmentOptionsVisibility; set { _appointmentOptionsVisibility = value; NotifyPropertyChanged(nameof(AppointmentOptionVisibility)); } }

        /// <summary>
        /// Die Termine für den Ausgewählten Tag
        /// </summary>
        public ObservableCollection<Appointment> DayAppointments { get => new ObservableCollection<Appointment>(AppointmentData.Appointments.Where(x => x.Date.Day == CurrentDate.Day && x.Date.Month == CurrentDate.Month && x.Date.Year == CurrentDate.Year)); }

        public CommandBase<object> DeleteCommand { get; set; }
        public CommandBase<object> ApplyCommand { get; set; }
        public CommandBase<object> AddCommand { get; set; }

        /// <summary>
        /// Das derzeitig ausgewählte Datum
        /// </summary>
        public DateTime CurrentDate { get => CalendarData.SelectedDate; }

        /// <summary>
        /// Liste aller Kalender
        /// </summary>
        public ObservableCollection<Calendar> Calendars { get => CalendarData.Calendars; }

        /// <summary>
        /// Der derzeitige ausgewählte Kalender
        /// </summary>
        public Calendar SelectedCalender { get => CalendarData.SelectedCalender; set => CalendarData.SelectedCalender = value;}

        #endregion
        #region Methoden
        /// <summary>
        /// Fügt einen neuen Termin hinzu
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Aktualisiert ein Termin
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateAppointment(object obj)
        {
            _appointmentData.UpdateItem(SelectedAppointment);
            AppointmentData.Appointments.Remove(SelectedAppointment);
            AppointmentData.Appointments.Add(SelectedAppointment);

            NotifyPropertyChanged(nameof(DayAppointments));
        }

        /// <summary>
        /// Entfernt ein Termin
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteAppointment(object obj)
        {
            _appointmentData.DeleteItem(SelectedAppointment);
            AppointmentData.Appointments.Remove(SelectedAppointment);   
            SelectedAppointment=null;

            NotifyPropertyChanged(nameof(DayAppointments));
        }
        #endregion
    }
}
