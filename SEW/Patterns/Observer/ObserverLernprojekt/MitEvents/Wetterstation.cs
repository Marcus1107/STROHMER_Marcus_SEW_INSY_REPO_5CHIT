namespace ObserverLernprojekt.MitEvents;

public class Wetterstation
{
    public int? Temperatur { get; private set; }

    public event EventHandler<TemperaturEventArgs>? TemperaturGeaendert;

    public void TemperaturSetzen(int neueTemperatur)
    {
        if (Temperatur == neueTemperatur)
            return;

        Temperatur = neueTemperatur;
        Console.WriteLine($"[Station] Neu: {Temperatur} °C");

        // this = Sender; das zweite Argument enthält die Messdaten.
        // ?. verhindert einen Aufruf, wenn niemand angemeldet ist.
        TemperaturGeaendert?.Invoke(
            this, new TemperaturEventArgs(neueTemperatur));
    }
}
