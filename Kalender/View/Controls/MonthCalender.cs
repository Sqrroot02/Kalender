using Kalender.Data;
using Kalender.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kalender.View.Controls
{
    /// <summary>
    /// Monats Kalender Control
    /// </summary>
    public class MonthCalender : Canvas
    {
        private readonly AppointmentData _appointmentData = new AppointmentData();

        public MonthCalender()
        {
            Appointments = new ObservableCollection<Appointment>();
            SizeChanged += MonthCalender_SizeChanged;
        }

        #region Dependency Properties
        /// <summary>
        /// Der zu Anzeigende Monat
        /// </summary>
        public int Month
        {
            get { return (int)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Month.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(int), typeof(MonthCalender), new PropertyMetadata(CallbackMonth));

        private static void CallbackMonth(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MonthCalender monthCalender = (MonthCalender)sender;
            if ((int)e.NewValue <= 0 || (int)e.NewValue > 12)
                monthCalender.Month = DateTime.Now.Month;
            else
                monthCalender.Month = (int)e.NewValue;
            monthCalender.DateChanged();
        }

        private static void CallbackYear(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MonthCalender monthCalender = (MonthCalender)sender;
            if ((int)e.NewValue == 0)
                monthCalender.Year = DateTime.Now.Year;
            else
                monthCalender.Year = (int)e.NewValue;
            monthCalender.DateChanged();
        }
        protected virtual void DateChanged()
        {            
            if (Year != 0 && Month != 0)
            {
                AppointmentData.Appointments = new ObservableCollection<Appointment>(_appointmentData.GetAppointmentsByDate(Year, Month));  
                CalendarData.SelectedDate = new DateTime(Year, Month, CalendarData.SelectedDate.Day);
                Children.Clear();
                
                BuildWeeks();
                BuildCalender();
            }            
        }

        /// <summary>
        /// Das zu Anzeigende Jahr
        /// </summary>
        public int Year
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Year.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(int), typeof(MonthCalender), new PropertyMetadata(CallbackYear));
        #endregion
        
        #region Event Methodes
 
        /// <summary>
        /// Aktualisiert den Kalender wenn die Größe vom Visual Parent verändert wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthCalender_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Year != 0 && Month != 0)
            {
                Children.Clear();
                BuildCalender();
            }
        }

        #endregion
        #region Properties        
        private ObservableCollection<WeekDate> _weekDates = new ObservableCollection<WeekDate>();
        /// <summary>
        /// WeekDate Controls, welche einen Tag im Monat visualisieren soll
        /// </summary>
        public ObservableCollection<WeekDate> WeekDates { get { return _weekDates; } set { _weekDates = value; } }

        /// <summary>
        /// Termine in diesem Monat
        /// </summary>
        public ObservableCollection<Appointment> Appointments { get; set; }
        
        #endregion

        #region Methodes
        /// <summary>
        /// Baut alle Wochenelemente im Kalender
        /// </summary>
        public void BuildWeeks()
        {
            WeekDates.Clear();

            DateTime currentDate = new DateTime(Year,Month,1);
            int posY = 1;
            for (int i = 1; i <= DateTime.DaysInMonth(currentDate.Year,currentDate.Month); i++)
            {
                WeekDate weekDate = new WeekDate();
                if (currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekDate.XPos = ((int)currentDate.DayOfWeek) - 1;
                    weekDate.YPos = posY;
                }
                else
                {
                    weekDate.XPos = 6;
                    weekDate.YPos= posY;
                    posY++;
                }
                weekDate.CurrentDate = currentDate;
                WeekDates.Add(weekDate);
                currentDate = currentDate.AddDays(1);
            }
        }

        /// <summary>
        /// Visualisiert den Kalender
        /// </summary>
        public void BuildCalender()
        {
            double widthFactor = (double)GetValue(ActualWidthProperty) / 7;
            double heightFactor = ((double)GetValue(ActualHeightProperty)) / 6.3;
            List<TextBlock> weekDays = WeekHeaders();

            for (int i = 0; i < weekDays.Count; i++)
            {
                SetLeft(weekDays[i], i * widthFactor);
                SetTop(weekDays[i], heightFactor - 20);

                Children.Add(weekDays[i]);
            }

            foreach (WeekDate item in WeekDates)
            {
                item.Height = heightFactor;
                item.Width = widthFactor;

                SetLeft(item, widthFactor * item.XPos);
                SetTop(item, heightFactor * item.YPos);
              
                Children.Add(item);
            }
        }

        /// <summary>
        /// Baut die Textblocke für die Wochennamen Anzeigen
        /// </summary>
        /// <returns></returns>
        private List<TextBlock> WeekHeaders()
        {
            List<TextBlock> list = new List<TextBlock>()
            {
                BuildTextblock("Monday"), BuildTextblock("Tuesday"), BuildTextblock("Wednesday"), BuildTextblock("Thursday"), BuildTextblock("Friday"), BuildTextblock("Saturday"), BuildTextblock("Sunday")
            };
            return list;
        }

        /// <summary>
        /// Baut die Textblocke für die Wochennamen Anzeigen
        /// </summary>
        /// <returns></returns>
        private TextBlock BuildTextblock(string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = Brushes.Black;
            textBlock.FontSize = 14;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
            return textBlock;
        }
        #endregion
    }
}
