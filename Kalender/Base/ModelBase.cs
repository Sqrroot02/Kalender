using System.ComponentModel;

namespace Kalender.Base
{
    /// <summary>
    /// Basisklasse für alle Models
    /// </summary>
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // View wird bei einer Änderung einer Eigenschaft aktualisiert
        protected void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
