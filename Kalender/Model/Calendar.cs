using Kalender.Base;
using System;
using System.Windows.Media;

namespace Kalender.Model
{
    /// <summary>
    /// Die Modelklasse für einen Kalernder
    /// </summary>
    public class Calendar : ModelBase
    {
        public Calendar()
        {

        }

        #region Eigenschaften

        private string _name = "";
        /// <summary>
        /// Der Name des Kalenders
        /// </summary>
        public string Name 
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private Guid _calendarId = Guid.NewGuid();
        /// <summary>
        /// Die ID des Kalenders
        /// </summary>
        public Guid CalendarId
        {
            get => _calendarId;
            set { _calendarId = value; NotifyPropertyChanged(nameof(CalendarId)); }
        }

        private Color _color = Colors.Red;
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

        #endregion
    }
}
