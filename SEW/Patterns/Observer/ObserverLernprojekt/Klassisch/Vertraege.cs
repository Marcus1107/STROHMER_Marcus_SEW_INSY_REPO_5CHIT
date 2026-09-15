namespace ObserverLernprojekt.Klassisch;

// Jeder Beobachter verspricht, auf diese Methode zu reagieren.
public interface ITemperaturBeobachter
{
    void Aktualisieren(int temperatur);
}

// Gemeinsamer Vertrag für eine beobachtbare Quelle.
public interface ITemperaturQuelle
{
    void Anmelden(ITemperaturBeobachter beobachter);
    void Abmelden(ITemperaturBeobachter beobachter);
}
