using Kalender.Data;
using Kalender.Model;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für AppointmentBar.xaml
    /// </summary>
    public partial class AppointmentBar : UserControl , INotifyPropertyChanged
    {
        public AppointmentBar(Appointment appointment)
        {
            InitializeComponent();
            Appointment = appointment;
            DataContext = Appointment;
            AppointmentData.OnSelectedAppointmentChanged += AppointmentData_OnSelectedAppointmentChanged;
            AppointmentData.Appointments.CollectionChanged += Appointments_CollectionChanged;
        }

        private void Appointments_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(Appointment));

        }

        private void AppointmentData_OnSelectedAppointmentChanged(object? sender, Appointment e)
        {
            if (AppointmentData.SelectedAppointment == Appointment)
                brd_Bar.BorderBrush = Brushes.Blue;
            else
                brd_Bar.BorderBrush= Brushes.Transparent;
        }

        private Appointment _appointment = null;
        public Appointment Appointment { get => _appointment; set { _appointment = value; NotifyPropertyChanged(nameof(Appointment)); } }

        private void usc_AppointmentBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppointmentData.SelectedAppointment = Appointment;
        }

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
