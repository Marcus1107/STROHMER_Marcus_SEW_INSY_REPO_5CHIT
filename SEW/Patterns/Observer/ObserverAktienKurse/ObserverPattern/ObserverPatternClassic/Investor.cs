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
        Console.WriteLine($"{Name} wurde informiert: {stock.Symbol} jetzt bei {stock._price}");
    }
}