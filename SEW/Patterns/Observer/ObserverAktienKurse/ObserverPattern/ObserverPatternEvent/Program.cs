using System;
using System.Text;

namespace ObserverPatternEvent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var random = new Random();

            var apple = new StockEvent("AAPL", 150.00m);
            var microsoft = new StockEvent("MSFT", 310.00m);
            var tesla = new StockEvent("TSLA", 230.00m);
            
            var ticker = new TickerDisplayEvent();
            
            var investorA = new InvestorEvent("Nowak");
            var investorB = new InvestorEvent("Lisa");
            var investorC = new InvestorEvent("Hofihasi");
            
            // ticker "abonnieren", sodass Meldung über Kursänderung ausgegeben wird
            apple.PriceChanged += ticker.OnPriceChanged;
            microsoft.PriceChanged += ticker.OnPriceChanged;
            tesla.PriceChanged += ticker.OnPriceChanged;
            
            // Investoren, welche apple "abonniert" haben
            apple.PriceChanged += investorA.OnPriceChanged;
            apple.PriceChanged += investorB.OnPriceChanged;
            
            // Investoren, welche microsoft "abonniert" haben
            microsoft.PriceChanged += investorA.OnPriceChanged;
            microsoft.PriceChanged += investorC.OnPriceChanged;
            
            // Investoren, welche tesla "abonniert" haben
            tesla.PriceChanged += investorB.OnPriceChanged;
            tesla.PriceChanged += investorC.OnPriceChanged;
            
            var stocks = new List<StockEvent> { apple, microsoft, tesla };
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"------------ Runde {i + 1} ------------");
                foreach (var stock in stocks)
                {
                    stock.Fluctuate(random);
                    Console.WriteLine();
                }
                Thread.Sleep(2000);
            }
            
            /*
            apple.PriceChanged += ticker.OnPriceChanged;
            
            // beide werden informiert
            apple.PriceChanged += investorA.OnPriceChanged;
            apple.PriceChanged += investorB.OnPriceChanged;

            apple.SetPrice(150.25m); // Preisänderung
            
            apple.PriceChanged -= investorB.OnPriceChanged; // Lisa wird nicht mehr benachrichtigt
            
            apple.SetPrice(170.00m); // Preisänderung --> nur noch Nowak wird informiert
            */
        }
    }
}