namespace ObserverPatternEvent;

/*public class PriceChangedEventArgs : EventArgs
{
    public decimal NewPrice { get; }

    public PriceChangedEventArgs(decimal newPrice)
    {
        NewPrice = newPrice;
    }
}*/

/*public class PriceChangedEventArgs(decimal newPrice) : EventArgs
{
    public decimal NewPrice { get; } = newPrice;
}*/

public class PriceChangedEventArgs(decimal newPrice) : EventArgs
{
    public string NewPrice { get; }
}


