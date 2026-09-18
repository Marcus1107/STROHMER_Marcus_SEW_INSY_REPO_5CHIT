namespace ObserverPatternEvent;

public class TickerDisplayEvent
{
    public void OnPriceChanged(object? sender, PriceChangedEventArgs e)
    {
        var stock = sender as StockEvent;
        Console.WriteLine($"[Ticker] {stock?.Symbol} Kurs aktualisiert auf {e.NewPrice}€");    
    }
}