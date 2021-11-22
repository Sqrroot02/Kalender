using Kalender.Base;
using Kalender.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kalender.Data
{
    /// <summary>
    /// Datenlogik für das Termin Model
    /// </summary>
    public sealed class AppointmentData : DataBase<Appointment>
    {
        /// <summary>
        /// Event für das neu auswählen eines Termines
        /// </summary>
        public static event EventHandler<Appointment>? OnSelectedAppointmentChanged;

        MySqlCommand _command = new MySqlCommand();
       
        /// <summary>
        /// Alle Termine die im derzeitigen Context relevant sind
        /// </summary>
        public static ObservableCollection<Appointment> Appointments = new ObservableCollection<Appointment>();
        private static Appointment _selectedAppointment = null;
        /// <summary>
        /// Der derzeitig Ausgewählte Termin
        /// </summary>
        public static Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set { _selectedAppointment = value; OnSelectedAppointmentChanged?.Invoke(value, value); }
        }

        public AppointmentData()
        {
            _command.Connection = DatabaseConnection.Connection;
        }

        /// <summary>
        /// Initialisiert die Tabelle "Appointment"
        /// </summary>
        public static void InitTables()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Connection = DatabaseConnection.Connection;
            mySqlCommand.Parameters.Clear();

            string sqlAppointment = "CREATE TABLE IF NOT EXISTS Appointment (appointmentId VARCHAR(56) PRIMARY KEY, Place VARCHAR(56), Title VARCHAR(512), Date DATETIME, HourStart INT, HourEnd INT, MinuteStart INT, MinuteEnd INT, WholeDay TINYINT(1), Description VARCHAR(2048), Color VARCHAR(45));";
            string sqlAppointmentCalender = "CREATE TABLE IF NOT EXISTS CalenderEntry (appointmentId VARCHAR(56), calenderId VARCHAR(56), IsVisible TINYINT(1))"; 

            mySqlCommand.CommandText = sqlAppointment + sqlAppointmentCalender;

            mySqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Löscht ein Termin aus der Appointment Tabelle
        /// </summary>
        /// <param name="item"></param>
        public override void DeleteItem(Appointment item)
        {
            string sql = @"DELETE FROM appointment WHERE appointmentId=@aid";
            _command.CommandText = sql;
            _command.Parameters.Clear();
            _command.Parameters.Add(new MySqlParameter("aid", item.AppointmentId));
            _command.ExecuteNonQuery();
        }

        public override void DeleteItems(List<Appointment> items)
        {
            throw new NotImplementedException();
        }

        public override Appointment GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Appointment> GetItems()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fügt einen neuen Termin hinzu
        /// </summary>
        /// <param name="item"></param>
        public override void InsertItem(Appointment item)
        {
            item.AppointmentColor = Colors.CadetBlue;
            string sql = @"INSERT INTO appointment (appointmentId,Place,Title,Date,WholeDay,Description,HourStart,HourEnd,MinuteStart,MinuteEnd,Color) VALUES (@aidappointment, @aplace,@atitle,@adatestart,@awholeday,@adescription,@ahourend,@ahourstart,@aminstart, @aminend,@acolor)";
            _command.CommandText = sql;
            _command.Parameters.Clear();

            _command.Parameters.Add(new MySqlParameter("aidappointment", item.AppointmentId));
            _command.Parameters.Add(new MySqlParameter("aplace", item.Location));
            _command.Parameters.Add(new MySqlParameter("atitle", item.Title));
            _command.Parameters.Add(new MySqlParameter("adatestart", item.Date));
            _command.Parameters.Add(new MySqlParameter("awholeday", ConvertBoolToTinyInt(item.FullTime)));
            _command.Parameters.Add(new MySqlParameter("adescription", item.Description));
            _command.Parameters.Add(new MySqlParameter("ahourend", item.HourEnd));
            _command.Parameters.Add(new MySqlParameter("ahourstart", item.HourStart));
            _command.Parameters.Add(new MySqlParameter("aminstart", item.MinuteStart));
            _command.Parameters.Add(new MySqlParameter("aminend", item.MinuteEnd));
            _command.Parameters.Add(new MySqlParameter("acolor", item.AppointmentColor.ToString()));

            _command.ExecuteNonQuery();
        }

        public override void InsertItems(List<Appointment> items)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Aktualisiert einen Termin
        /// </summary>
        /// <param name="item"></param>
        public override void UpdateItem(Appointment item)
        {
            string sql = @"UPDATE appointment SET Place=@aplace,Title=@atitle,Date=@adatestart,WholeDay=@awholeday,Description=@adescription,HourStart=@ahstart,HourEnd=@ahend,MinuteStart=@amstart,MinuteEnd=@amend,Color=@acolor WHERE appointmentId=@aidappointment";
            _command.CommandText = sql;
            _command.Parameters.Clear();
            
            _command.Parameters.Add(new MySqlParameter("aidappointment", item.AppointmentId));
            _command.Parameters.Add(new MySqlParameter("aplace", item.Location));
            _command.Parameters.Add(new MySqlParameter("atitle", item.Title));
            _command.Parameters.Add(new MySqlParameter("adatestart", item.Date));
            _command.Parameters.Add(new MySqlParameter("awholeday", ConvertBoolToTinyInt(item.FullTime)));
            _command.Parameters.Add(new MySqlParameter("adescription", item.Description));
            _command.Parameters.Add(new MySqlParameter("ahstart", item.HourStart));
            _command.Parameters.Add(new MySqlParameter("ahend", item.HourEnd));
            _command.Parameters.Add(new MySqlParameter("amstart", item.MinuteStart));
            _command.Parameters.Add(new MySqlParameter("amend", item.MinuteEnd));
            _command.Parameters.Add(new MySqlParameter("acolor", item.AppointmentColor.ToString()));

            _command.ExecuteNonQuery();
        }

        /// <summary>
        /// Mappt einen Termin aus der Relationalen Datenbank zum objektorientierten Schema
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Appointment> MapAppointments(DataTable dt)
        {
            List<Appointment> list = new List<Appointment>();
            foreach (DataRow item in dt.Rows)
            {
                Appointment appointment = new Appointment
                {
                    Title = item["Title"].ToString(),
                    AppointmentId = Guid.Parse(item["appointmentId"].ToString()),
                    Location = item["Place"].ToString(),
                    Date = DateTime.Parse(item["Date"].ToString()),
                    FullTime = ConvertTinyIntToBool(Convert.ToByte(item["WholeDay"])),
                    Description = item["Description"].ToString(),
                    HourStart = Convert.ToInt32(item["HourStart"]),
                    HourEnd = Convert.ToInt32(item["HourEnd"]),
                    MinuteEnd = Convert.ToInt32(item["MinuteEnd"]),
                    MinuteStart = Convert.ToInt32(item["MinuteStart"]),
                    AppointmentColor = (Color)ColorConverter.ConvertFromString(item["Color"].ToString())
                };
                list.Add(appointment);
            }
            return list;    
        }

        /// <summary>
        /// Gibt alle Termine nach einem ausgewähltem Datum wieder
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<Appointment> GetAppointmentsByDate(int year, int month)
        {
            List<Appointment> list = new List<Appointment>();
            string sql = @"SELECT * FROM appointment WHERE YEAR(Date)=@ayear AND MONTH(Date)=@amonth";
            _command.CommandText = sql;
            _command.Parameters.Clear();

            _command.Parameters.Add((new MySqlParameter("ayear", year)));
            _command.Parameters.Add((new MySqlParameter("amonth", month)));

            using (MySqlDataAdapter adapter = new MySqlDataAdapter())
            {
                adapter.SelectCommand = _command;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                list = MapAppointments(dt);
            }
            return list;
        }

        /// <summary>
        /// Gibt alle Termine nach einem ausgewähltem Datum wieder
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<Appointment> GetAppointmentsByDate(int year, int month, int day)
        {
            List<Appointment> list = new List<Appointment>();
            string sql = @"SELECT * FROM appointment WHERE YEAR(Date)=@ayear AND MONTH(Date)=@amonth AND DAY(DATE)=@aday";
            _command.CommandText = sql;
            _command.Parameters.Clear();

            _command.Parameters.Add((new MySqlParameter("ayear", year)));
            _command.Parameters.Add((new MySqlParameter("amonth", month)));
            _command.Parameters.Add((new MySqlParameter("aday", day)));

            using (MySqlDataAdapter adapter = new MySqlDataAdapter())
            {
                adapter.SelectCommand = _command;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                list = MapAppointments(dt);
            }
            return list;
        }
    }
}
