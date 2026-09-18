namespace ObserverPatternClassic;

public class Stock : ISubject
{
    private readonly List<IObserver> _observers = new();
    private decimal _price;
    public string Symbol { get; }
    
    public Stock(string symbol, decimal startPrice)
    {
        Symbol = symbol;
        _price = startPrice;
    }
    
    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this, _price);
        }
    }
    
    public void SetPrice(decimal price)
    {
        _price = price;
        Notify();
    }
    
    public void Fluctuate(Random random)
    {
        int change = random.Next(-5, 6);
        decimal newPrice = _price + change;

        if (newPrice < 1)
        {
            newPrice = 1;
        }

        SetPrice(newPrice);
    }
}