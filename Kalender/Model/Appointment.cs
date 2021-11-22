using Kalender.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kalender.Model
{
    public class Appointment : ModelBase
    {
        public Appointment()
        {

        }

        #region Eigenschaften

        private Guid _appointmentId;
        /// <summary>
        /// Die ID des Termines
        /// </summary>
        public Guid AppointmentId
        {
            get { return _appointmentId; }
            set { _appointmentId = value; NotifyPropertyChanged(nameof(AppointmentId)); }
        }

        private DateTime _dateStart = DateTime.Now;
        /// <summary>
        /// Das Datum und die Zeit an dem der Termin startet
        /// </summary>
        public DateTime Date
        {
            get { return _dateStart; }
            set { _dateStart = value; NotifyPropertyChanged(nameof(Date)); }
        }


        private string _description = "";
        /// <summary>
        /// Die Beschreibung des Termines
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(nameof(Description));}
        }

        private string _location = "";
        /// <summary>
        /// Der Ort an dem der Termin stattfindet
        /// </summary>
        public string Location
        {
            get { return _location; }
            set { _location = value; NotifyPropertyChanged(nameof(Location));}
        }

        private bool _fullTime = false;
        /// <summary>
        /// Ist keine bestimmte Uhrzeit gegeben, kann ein Termin auch ganztägig stattfinden
        /// </summary>
        public bool FullTime
        {
            get { return _fullTime; }
            set { _fullTime = value; NotifyPropertyChanged(nameof(FullTime));}
        }

        private Color _appointmentColor = Colors.White;
        /// <summary>
        /// Die Farbe in welchem der Termin angezeigt werden soll
        /// </summary>
        public Color AppointmentColor { get { return _appointmentColor; } set { _appointmentColor = value; NotifyPropertyChanged(nameof(AppointmentColor));} }

        private string _title = "";
        /// <summary>
        /// Der Titel des Termines
        /// </summary>
        public string Title { get => _title; set { _title = value; NotifyPropertyChanged(nameof(Title));} }

        private int _hourStart = 0;
        public int HourStart { get { return _hourStart; } set { _hourStart = value;NotifyPropertyChanged(nameof(HourStart));} }

        private int _minuteStart = 0;
        public int MinuteStart { get { return _minuteStart; } set { _minuteStart = value; NotifyPropertyChanged(nameof(MinuteStart));} }

        private int _hourEnd = 0;
        public int HourEnd { get { return _hourEnd; } set { _hourEnd = value; NotifyPropertyChanged(nameof(HourEnd));} }

        private int _minuteEnd = 0;
        public int MinuteEnd { get { return _minuteEnd; } set { _minuteEnd = value; NotifyPropertyChanged(nameof(MinuteEnd));} }
        #endregion
    }
}
