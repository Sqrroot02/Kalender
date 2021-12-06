using Kalender.Base;
using Kalender.Data;
using System;
using System.Windows.Media;

namespace Kalender.Model
{
    /// <summary>
    /// Model für einen Termin
    /// </summary>
    public class Appointment : ModelBase
    {
        private readonly AppointmentData _appointmentData = new AppointmentData();
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
            set { _dateStart = value; NotifyPropertyChanged(nameof(Date)); _appointmentData.UpdateItem(this); }
        }


        private string _description = "";
        /// <summary>
        /// Die Beschreibung des Termines
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(nameof(Description)); _appointmentData.UpdateItem(this); }
        }

        private string _location = "";
        /// <summary>
        /// Der Ort an dem der Termin stattfindet
        /// </summary>
        public string Location
        {
            get { return _location; }
            set { _location = value; NotifyPropertyChanged(nameof(Location)); _appointmentData.UpdateItem(this); }
        }

        private bool _fullTime = true;
        /// <summary>
        /// Ist keine bestimmte Uhrzeit gegeben, kann ein Termin auch ganztägig stattfinden
        /// </summary>
        public bool FullTime
        {
            get { return _fullTime; }
            set { _fullTime = value; NotifyPropertyChanged(nameof(FullTime)); _appointmentData.UpdateItem(this); }
        }

        //private Color _appointmentColor = Colors.White;
        ///// <summary>
        ///// Die Farbe in welchem der Termin angezeigt werden soll
        ///// </summary>
        //public Color AppointmentColor { get { return _appointmentColor; } set { _appointmentColor = value; NotifyPropertyChanged(nameof(AppointmentColor));} }

        private string _title = "";
        /// <summary>
        /// Der Titel des Termines
        /// </summary>
        public string Title { get => _title; set { _title = value; NotifyPropertyChanged(nameof(Title)); _appointmentData.UpdateItem(this); } }

        private int _hourStart = 0;
        /// <summary>
        /// Die Stunde an dem der Termin startet
        /// </summary>
        public int HourStart { get { return _hourStart; } set { _hourStart = value;NotifyPropertyChanged(nameof(HourStart)); _appointmentData.UpdateItem(this); } }

        private int _minuteStart = 0;
        /// <summary>
        /// Die Minute an dem der Termin startet
        /// </summary>
        public int MinuteStart { get { return _minuteStart; } set { _minuteStart = value; NotifyPropertyChanged(nameof(MinuteStart)); _appointmentData.UpdateItem(this); } }

        private int _hourEnd = 0;
        /// <summary>
        /// Die Stunde an dem der Termin endet
        /// </summary>
        public int HourEnd { get { return _hourEnd; } set { _hourEnd = value; NotifyPropertyChanged(nameof(HourEnd)); _appointmentData.UpdateItem(this); } }

        private int _minuteEnd = 0;
        /// <summary>
        /// Die Minute an dem der Termin endet
        /// </summary>
        public int MinuteEnd { get { return _minuteEnd; } set { _minuteEnd = value; NotifyPropertyChanged(nameof(MinuteEnd)); _appointmentData.UpdateItem(this); } }
        #endregion
    }
}
