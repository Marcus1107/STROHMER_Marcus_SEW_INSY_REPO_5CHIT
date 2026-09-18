namespace ObserverPatternEvent;

public class PriceChangedEventArgs : EventArgs
{
    public decimal NewPrice { get; }

    public PriceChangedEventArgs(decimal newPrice)
    {
        NewPrice = newPrice;
    }
}