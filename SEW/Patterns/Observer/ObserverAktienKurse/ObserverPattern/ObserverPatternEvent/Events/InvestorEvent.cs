namespace ObserverPatternEvent;

public class InvestorEvent
{
    public string Name { get; }

    public InvestorEvent(string name)
    {
        Name = name;
    }

    public void OnPriceChanged(object? sender, PriceChangedEventArgs e)
    {
        var stock = sender as StockEvent;
        Console.WriteLine($"{Name} wurde informiert: {stock?.Symbol} jetzt bei {e.NewPrice}€");
    }
}