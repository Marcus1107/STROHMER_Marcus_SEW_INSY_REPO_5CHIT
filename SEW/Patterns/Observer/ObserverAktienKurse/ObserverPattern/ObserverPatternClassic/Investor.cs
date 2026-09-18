namespace ObserverPatternClassic;

public class Investor : IObserver
{
    private string Name;

    public Investor(string name)
    {
        Name = name;
    }

    public void Update(Stock stock, decimal price)
    {
        Console.WriteLine($"{Name} wurde informiert: {stock.Symbol} jetzt bei {price:C}");
    }
}