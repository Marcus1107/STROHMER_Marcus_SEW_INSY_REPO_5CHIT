namespace ObserverPatternClassic;

public class TickerDisplay : IObserver
{
    public void Update(Stock stock)
    {
        Console.WriteLine($"[{stock.LastUpdated:dd.MM.yyyy HH:mm:ss}] [Ticker] {stock.Symbol} Kurs aktualisiert auf {stock._price}€");
    }
}
