using K = ObserverLernprojekt.Klassisch;
using E = ObserverLernprojekt.MitEvents;

namespace ObserverLernprojekt;

public static class Demo
{
    public static void Klassisch()
    {
        Console.WriteLine("=== Klassisches Observer Pattern ===");
        var station = new K.Wetterstation();
        var anzeige = new K.TemperaturAnzeige();
        var alarm = new K.HitzeAlarm();

        station.Anmelden(anzeige);
        station.Anmelden(alarm);

        station.TemperaturSetzen(22);
        station.TemperaturSetzen(31);

        Console.WriteLine("--- Anzeige wird abgemeldet ---");
        station.Abmelden(anzeige);
        station.TemperaturSetzen(35);

        Console.WriteLine("--- Gleicher Wert: keine Meldung ---");
        station.TemperaturSetzen(35);

        Console.WriteLine("--- Auch Alarm wird abgemeldet ---");
        station.Abmelden(alarm);
        station.TemperaturSetzen(36);
    }

    public static void MitEvents()
    {
        Console.WriteLine("=== Observer mit C#-Events ===");
        var station = new E.Wetterstation();
        var anzeige = new E.TemperaturAnzeige();
        var alarm = new E.HitzeAlarm();

        station.TemperaturGeaendert += anzeige.BeiTemperaturAenderung;
        station.TemperaturGeaendert += alarm.BeiTemperaturAenderung;

        station.TemperaturSetzen(22);
        station.TemperaturSetzen(31);

        Console.WriteLine("--- Anzeige wird abgemeldet ---");
        station.TemperaturGeaendert -= anzeige.BeiTemperaturAenderung;
        station.TemperaturSetzen(35);

        Console.WriteLine("--- Gleicher Wert: keine Meldung ---");
        station.TemperaturSetzen(35);

        Console.WriteLine("--- Auch Alarm wird abgemeldet ---");
        station.TemperaturGeaendert -= alarm.BeiTemperaturAenderung;
        station.TemperaturSetzen(36);
    }
}
