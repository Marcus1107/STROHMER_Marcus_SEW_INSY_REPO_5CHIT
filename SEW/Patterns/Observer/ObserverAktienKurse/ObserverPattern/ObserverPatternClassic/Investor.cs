namespace ObserverPatternClassic;

public class Investor : IObserver
{
    private string Name;

    public Investor(string name)
    {
        Name = name;
    }

    public void Update(Stock stock)
    {
        Console.WriteLine($"[{stock.LastUpdated:dd.MM.yyyy HH:mm:ss}] {Name} wurde informiert: {stock.Symbol} jetzt bei {stock._price}");
    }
}
