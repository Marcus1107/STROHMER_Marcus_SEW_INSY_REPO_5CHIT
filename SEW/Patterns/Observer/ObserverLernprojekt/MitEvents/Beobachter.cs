namespace ObserverLernprojekt.MitEvents;

public class TemperaturAnzeige
{
    public void BeiTemperaturAenderung(
        object? sender, TemperaturEventArgs e)
    {
        Console.WriteLine($"[Anzeige] {e.Temperatur} °C");
    }
}

public class HitzeAlarm
{
    public void BeiTemperaturAenderung(
        object? sender, TemperaturEventArgs e)
    {
        if (e.Temperatur >= 30)
            Console.WriteLine($"[Alarm] Achtung: {e.Temperatur} °C!");
    }
}
