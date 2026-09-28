namespace ObserverPatternClassic;

public class TickerDisplay : IObserver
{
    public void Update(Stock stock)
    {
        Console.WriteLine($"[Ticker] {stock.Symbol} Kurs aktualisiert auf {stock._price}€");
    }
}