using Kalender.Base;
using Kalender.Data;
using System;
using System.Linq;
using System.Windows.Media;

namespace Kalender.Model
{
    /// <summary>
    /// Die Modelklasse für einen Kalender
    /// </summary>
    public class Calendar : ModelBase
    {
        private readonly CalendarData _data = new CalendarData();

        public Calendar()
        {
            
        }

        #region Eigenschaften

        private string _name = "New Calender";
        /// <summary>
        /// Der Name des Kalenders
        /// </summary>
        public string Name 
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(nameof(Name)); _data.UpdateItem(this); }
        }

        private Guid _calendarId = Guid.NewGuid();
        /// <summary>
        /// Die ID des Kalenders
        /// </summary>
        public Guid CalendarId
        {
            get => _calendarId;
            set { _calendarId = value; NotifyPropertyChanged(nameof(CalendarId)); _data.UpdateItem(this); }
        }

        private Color _color;
        /// <summary>
        /// Die Farbe des Kalenders
        /// </summary>
        public Color Color
        {
            get => _color;
            set { _color = value; NotifyPropertyChanged(nameof(Color));}
        }

        private DateTime _timeCreated = DateTime.Now;
        /// <summary>
        /// Der Zeitpunkt an dem Der Kalender Erstellt wurde
        /// </summary>
        public DateTime TimeCreated
        {
            get => _timeCreated;
            set { _timeCreated = value; NotifyPropertyChanged(nameof(TimeCreated)); }
        }

        private bool _isVisible = true;
        /// <summary>
        /// Soll Termine dieses Kalenders angezeigt werden oder nicht?
        /// </summary>
        public bool IsVisible 
        { 
            get => _isVisible;
            set 
            { 
                _isVisible = value; 
                NotifyPropertyChanged(nameof(IsVisible));
                if (value == false)
                {
                    var appointments = AppointmentData.Appointments.Where(x => x.CalenderId == CalendarId).ToList();
                    foreach (var item in appointments)
                        AppointmentData.Appointments.Remove(item);
                }
            }
        }

        #endregion
    }
}
