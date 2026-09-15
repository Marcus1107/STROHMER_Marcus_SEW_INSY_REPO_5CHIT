namespace ObserverLernprojekt.Klassisch;

public class Wetterstation : ITemperaturQuelle
{
    private readonly List<ITemperaturBeobachter> beobachter = new();

    public int? Temperatur { get; private set; }

    public void Anmelden(ITemperaturBeobachter neuerBeobachter)
    {
        ArgumentNullException.ThrowIfNull(neuerBeobachter);
        // Dieselbe Instanz soll nicht doppelt angemeldet werden.
        if (!beobachter.Contains(neuerBeobachter))
            beobachter.Add(neuerBeobachter);
    }

    public void Abmelden(ITemperaturBeobachter alterBeobachter)
    {
        beobachter.Remove(alterBeobachter);
    }

    public void TemperaturSetzen(int neueTemperatur)
    {
        // Nur Änderungen werden gemeldet, nicht gleiche Folgemessungen.
        if (Temperatur == neueTemperatur)
            return;

        Temperatur = neueTemperatur;
        Console.WriteLine($"[Station] Neu: {Temperatur} °C");
        Benachrichtigen(neueTemperatur);
    }

    private void Benachrichtigen(int neueTemperatur)
    {
        // Momentaufnahme: Abmelden im Callback verändert diese Kopie nicht.
        foreach (ITemperaturBeobachter b in beobachter.ToArray())
            b.Aktualisieren(neueTemperatur);
    }
}
