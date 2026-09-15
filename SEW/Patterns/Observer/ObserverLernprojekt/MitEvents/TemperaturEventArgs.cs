namespace ObserverLernprojekt.MitEvents;

// Ein Datenpaket für alle Empfänger dieser Benachrichtigung.
public class TemperaturEventArgs : EventArgs
{
    public int Temperatur { get; }

    public TemperaturEventArgs(int temperatur)
    {
        Temperatur = temperatur;
    }
}
