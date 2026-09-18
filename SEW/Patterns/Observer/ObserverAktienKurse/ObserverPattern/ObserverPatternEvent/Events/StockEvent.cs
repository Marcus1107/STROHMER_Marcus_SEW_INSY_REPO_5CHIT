namespace ObserverPatternEvent;

public class StockEvent
{
    public string Symbol { get; }
    private decimal _price;

    public event EventHandler<PriceChangedEventArgs>? PriceChanged;

    public StockEvent(string symbol, decimal startPrice)
    {
        Symbol = symbol;
        _price = startPrice;
    }

    public void SetPrice(decimal price)
    {
        _price = price;
        OnPriceChanged(new PriceChangedEventArgs(price));
    }

    public void Fluctuate(Random random)
    {
        int change = random.Next(-5, 6);
        decimal newPrice = _price + change;

        if (newPrice < 1)
        {
            newPrice = 1; // Preis soll nicht negativ/null werden
        }

        SetPrice(newPrice);
    }
    
    protected virtual void OnPriceChanged(PriceChangedEventArgs e)
    {
        PriceChanged?.Invoke(this, e);
    }
}