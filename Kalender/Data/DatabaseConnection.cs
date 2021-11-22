using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Kalender.Data
{
    /// <summary>
    /// Statische J
    /// </summary>
    public static class DatabaseConnection
    {
        public static string Database = "";
        public static string UserName = "";
        public static string Password = "";
        public static string Port = "";
        public static string SSLMode = "";
        public static string Host = "";

        private static MySqlConnection _connection = new MySqlConnection();
        public static MySqlConnection Connection { get => _connection; private set => _connection = value; }

        /// <summary>
        /// Erstellt eine MySQL Verbindung
        /// </summary>
        public static void CreateConnection()
        {
            string connectionString = $@"host={Host};user={UserName};password={Password};port={Port};database={Database};SSL Mode={SSLMode}";
            Connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Verbindet sich mit der Datebank
        /// </summary>
        public static void Connect()
        {
            try
            {
                Connection.Open();
                InitDatabase();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed on connecting to Database... " + e);
                Console.WriteLine("Failed on connecting to Database... Further Informations: " + e.Message);
                Disconnect();
            }
        }

        /// <summary>
        /// Schließt die Verbindung zur Datenbank
        /// </summary>
        public static void Disconnect()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed on disconnecting to Database... EXCEPTION: " + e.Message);
            }
        }

        /// <summary>
        /// Initialisiert die Datenbank mit allen Tabellen bei der ersten ausführung
        /// </summary>
        public static void InitDatabase()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = Connection;
            string sql = "CREATE DATABASE IF NOT EXISTS Calendar";
            command.CommandText = sql;
            command.ExecuteNonQuery();

            CalendarData.InitTables();
            AppointmentData.InitTables();
            NotificationData.InitTables();
            UserData.InitTables();  
        }
    }
}
