using Kalender.Data;
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
            // ! Hier eigene Conncetion Daten eintragen !
            DatabaseConnection.Host = "localhost";
            DatabaseConnection.Database = "calendar";
            DatabaseConnection.UserName = "root";
            DatabaseConnection.Password = "kuzsgcebvtwiuiguiuzgrwzuer";
            DatabaseConnection.SSLMode = "0";
            DatabaseConnection.Port = "3306";

            // Erstellt die Connection und Verbindet sich mit der Datenbank
            DatabaseConnection.CreateConnection();
            DatabaseConnection.Connect();
        }

        // Beim schließen der Anwendung wird die Verbindung zur DB ebenfalls getrennt
        private void App_Exit(object sender, ExitEventArgs e) =>
            DatabaseConnection.Disconnect();
    }
}
