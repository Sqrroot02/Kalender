using Kalender.Data;
using Kalender.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            InitCommand();
            Appointments.CollectionChanged += Appointments_CollectionChanged;
        }

        private void Appointments_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            AppointmentData.Appointments = Appointments;
        }

        public ObservableCollection<Appointment> Appointments { get => AppointmentData.Appointments;}

        public event PropertyChangedEventHandler? PropertyChanged;

        // View wird bei einer Änderung einer Eigenschaft aktualisiert
        protected void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public virtual void InitCommand() { }

    }
}
