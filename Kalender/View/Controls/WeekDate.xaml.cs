using Kalender.Data;
using Kalender.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalender.View.Controls
{
    /// <summary>
    /// Interaktionslogik für WeekDate.xaml
    /// </summary>
    public partial class WeekDate : UserControl, INotifyPropertyChanged
    {
        public WeekDate()
        {
            InitializeComponent();
            AppointmentData.Appointments.CollectionChanged += Appointments_CollectionChanged;
            AppointmentData.OnSelectedAppointmentChanged += AppointmentData_OnSelectedAppointmentChanged;
            CalendarData.SelectedDateChanged += CalendarData_SelectedDateChanged;
            DataContext = this;
        }

        private void CalendarData_SelectedDateChanged(object? sender, DateTime e)
        {
            if (e.Month == CurrentDate.Month && e.Day == CurrentDate.Day && e.Year == CurrentDate.Year)
                brd_WeekDateBorder.BorderBrush = Brushes.MediumBlue;
            else
                brd_WeekDateBorder.BorderBrush= Brushes.Gray;
        }

        private void AppointmentData_OnSelectedAppointmentChanged(object? sender, Appointment e)
        {            
            if (Appointments.Contains(SelectedAppointment))
                NotifyPropertyChanged(nameof(SelectedAppointment));                   
        }

        private void Appointments_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            NotifyPropertyChanged(nameof(Appointments));
        private void lib_Appointments_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            CalendarData.SelectedDate = CurrentDate;

        private void lib_Appointments_GotFocus(object sender, RoutedEventArgs e)
        {
            CalendarData.SelectedDate = CurrentDate;
            if (lib_Appointments.Items.Count == 1 && lib_Appointments.Items.Count != 0)
            {
                SelectedAppointment = (Appointment)lib_Appointments.Items[0];
            }
        }
            

        public Border VisualBorder { get; set; }

        private int _posX = 0;
        public int XPos { get => _posX; set => _posX = value; }

        private int _posY = 0;
        public int YPos { get => _posY; set => _posY = value; }

        public Appointment SelectedAppointment { get => AppointmentData.SelectedAppointment; set => AppointmentData.SelectedAppointment = value; }

        private ObservableCollection<Appointment> _appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments { get => new ObservableCollection<Appointment>(AppointmentData.Appointments.Where(x => x.Date.Year == CurrentDate.Year && x.Date.Month == CurrentDate.Month && x.Date.Day == CurrentDate.Day)); }

        public DateTime CurrentDate { get; set; }

        public bool IsToday
        {
            get
            {
                if (CurrentDate == DateTime.Today) return true;
                else return false;
            }
        }

        public Visibility IsTodayVisible
        {
            get
            {
                if (!IsToday) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) =>
            CalendarData.SelectedDate = CurrentDate;

        public event PropertyChangedEventHandler? PropertyChanged;

        // View wird bei einer Änderung einer Eigenschaft aktualisiert
        protected void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }       
    }
}
