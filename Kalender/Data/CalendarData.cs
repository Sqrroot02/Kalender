using Kalender.Base;
using Kalender.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.Data
{
    public sealed class CalendarData : DataBase<Calendar>
    {
        /// <summary>
        /// Event für Änderungen beim ausgewähltem Datum
        /// </summary>
        public static event EventHandler<DateTime>? SelectedDateChanged;

        /// <summary>
        /// Das derzeitige Ausgewählte Datum
        /// </summary>
        private static DateTime _selectedDate = DateTime.Now;
        public static DateTime SelectedDate 
        { 
            get => _selectedDate;
            set { _selectedDate = value; SelectedDateChanged?.Invoke(value,value); }
        }
        public override void DeleteItem(Calendar item)
        {

        }

        public override void DeleteItems(List<Calendar> items)
        {
            throw new NotImplementedException();
        }

        public override Calendar GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Calendar> GetItems()
        {
            throw new NotImplementedException();
        }

        public static void InitTables()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Connection = DatabaseConnection.Connection;
            mySqlCommand.Parameters.Clear();

            string sql = "CREATE TABLE IF NOT EXISTS Calender (calenderId VARCHAR(56) PRIMARY KEY, Color VARCHAR(56), userId VARCHAR(56), DateCreated DATETIME);";
            mySqlCommand.CommandText = sql;
            mySqlCommand.ExecuteNonQuery();
        }

        public override void InsertItem(Calendar item)
        {
            throw new NotImplementedException();
        }

        public override void InsertItems(List<Calendar> items)
        {
            throw new NotImplementedException();
        }

        public override void UpdateItem(Calendar item)
        {
            throw new NotImplementedException();
        }
    }
}
