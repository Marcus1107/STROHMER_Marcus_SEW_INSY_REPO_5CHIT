namespace ObserverPatternClassic;

public interface IObserver
{
    void Update(Stock stock, decimal price);
}