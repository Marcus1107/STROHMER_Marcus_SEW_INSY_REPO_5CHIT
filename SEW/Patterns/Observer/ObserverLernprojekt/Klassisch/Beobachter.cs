namespace ObserverLernprojekt.Klassisch;

public class TemperaturAnzeige : ITemperaturBeobachter
{
    public void Aktualisieren(int temperatur)
    {
        Console.WriteLine($"[Anzeige] {temperatur} °C");
    }
}

public class HitzeAlarm : ITemperaturBeobachter
{
    public void Aktualisieren(int temperatur)
    {
        if (temperatur >= 30)
            Console.WriteLine($"[Alarm] Achtung: {temperatur} °C!");
    }
}
