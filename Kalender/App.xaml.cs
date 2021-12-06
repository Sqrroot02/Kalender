using Kalender.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kalender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Initialisiert Datenbank beim Starten des Kalenders
        public App()
        {
            Exit += App_Exit;

            // Initialisiert Connection Data für die Datenbank
            DatabaseConnection.Host = "localhost";
            DatabaseConnection.Database = "calendar";
            DatabaseConnection.UserName = "root";
            DatabaseConnection.Password = "kuzsgcebvtwiuiguiuzgrwzuer";
            DatabaseConnection.SSLMode = "0";
            DatabaseConnection.Port = "3301";

            // Erstellt die Connection und Verbindet sich mit der Datenbank
            DatabaseConnection.CreateConnection();
            DatabaseConnection.Connect();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            DatabaseConnection.Disconnect();
        }
    }
}
