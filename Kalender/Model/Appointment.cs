using Kalender.Base;
using Kalender.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;

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

        private Guid _calenderId = Guid.NewGuid();  
        /// <summary>
        /// Die ID der dazugehörigem Kalenders
        /// </summary>
        public Guid CalenderId 
        { 
            get { return _calenderId; }  
            set { _calenderId = value; NotifyPropertyChanged(nameof(CalenderId)); _appointmentData.UpdateItem(this); NotifyPropertyChanged(nameof(AppoinmentBrush)); }
        }

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
        public int HourStart 
        { 
            get { return _hourStart; } 
            set 
            {
                if (value < 23 && value >= 0)
                    _hourStart = value;
                else if (value < 0)
                    _hourStart = 0;
                else if (value >= 24)
                    _hourStart = 23;

                TimeOnly tEnd = new TimeOnly(HourEnd, MinuteEnd);
                TimeOnly tStart = new TimeOnly(_hourStart, MinuteStart);

                if (tEnd < tStart)
                {
                    _hourStart = tEnd.Hour; MinuteStart = tEnd.Minute;
                }

                NotifyPropertyChanged(nameof(HourStart));
                NotifyPropertyChanged(nameof(TimeSpan));

                _appointmentData.UpdateItem(this); 
            } 
        }

        private int _minuteStart = 0;
        /// <summary>
        /// Die Minute an dem der Termin startet
        /// </summary>
        public int MinuteStart 
        { 
            get { return _minuteStart; } 
            set 
            {
                if (value < 60 && value >= 0)
                    _minuteStart = value;
                else if (value < 0)
                    _minuteStart = 0;
                else if (value >= 60)
                    _minuteStart = 59;

                TimeOnly tEnd = new TimeOnly(HourEnd, MinuteEnd);
                TimeOnly tStart = new TimeOnly(HourStart, _minuteStart);

                if (tEnd < tStart)
                {
                    HourStart = tEnd.Hour; _minuteStart = tEnd.Minute;
                }

                NotifyPropertyChanged(nameof(MinuteStart)); 
                NotifyPropertyChanged(nameof(TimeSpan));

                _appointmentData.UpdateItem(this); 
            } 
        }

        private int _hourEnd = 0;
        /// <summary>
        /// Die Stunde an dem der Termin endet
        /// </summary>
        public int HourEnd 
        { 
            get { return _hourEnd; } 
            set 
            {
                if (value < 23 && value >= 0)
                    _hourEnd = value;
                else if (value < 0)
                    _hourEnd = 23;
                else if (value >= 24)
                    _hourEnd = 23;

                TimeOnly tEnd = new TimeOnly(_hourEnd, MinuteEnd);
                TimeOnly tStart = new TimeOnly(HourStart, MinuteStart);

                if (tEnd < tStart)
                {
                    _hourEnd = tStart.Hour; MinuteEnd = tStart.Minute;
                }

                NotifyPropertyChanged(nameof(HourEnd));
                NotifyPropertyChanged(nameof(TimeSpan));

                _appointmentData.UpdateItem(this); 
            } 
        }

        private int _minuteEnd = 0;
        /// <summary>
        /// Die Minute an dem der Termin endet
        /// </summary>
        public int MinuteEnd 
        { 
            get { return _minuteEnd; } 
            set 
            {
                if (value < 60 && value >= 0)
                    _minuteEnd = value;
                else if (value < 0)
                    _minuteEnd = 0;
                else if (value >= 60)
                    _minuteEnd = 59;

                TimeOnly tEnd = new TimeOnly(HourEnd, _minuteEnd);
                TimeOnly tStart = new TimeOnly(HourStart, MinuteStart);

                if (tEnd < tStart)
                {
                    HourEnd = tStart.Hour; _minuteEnd = tStart.Minute;
                }

                NotifyPropertyChanged(nameof(MinuteEnd));
                NotifyPropertyChanged(nameof(TimeSpan));

                _appointmentData.UpdateItem(this); 
            } 
        }

        #region Visual Properties

        public string TimeSpan
        {
            get
            {
                TimeOnly timeStart = new TimeOnly(HourStart,MinuteStart);
                TimeOnly timeEnd = new TimeOnly(HourEnd,MinuteEnd);
                return timeStart.ToShortTimeString() + " - " + timeEnd.ToShortTimeString();
            }
        }

        public SolidColorBrush AppoinmentBrush
        {
            get 
            {
                var calender = CalendarData.Calendars.Where(x => x.CalendarId == CalenderId).FirstOrDefault();
                return calender == null ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(calender.Color);
            }
        }
        
        #endregion
        #endregion
    }
}
