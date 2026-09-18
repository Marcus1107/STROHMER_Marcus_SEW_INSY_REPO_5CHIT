namespace ObserverPatternClassic;

public class TickerDisplay : IObserver
{
    public void Update(Stock stock, decimal price)
    {
        Console.WriteLine($"[Ticker] {stock.Symbol} Kurs aktualisiert auf {price}€");
    }
}