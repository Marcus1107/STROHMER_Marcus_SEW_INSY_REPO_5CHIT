using System;
using System.Text;

namespace ObserverPatternClassic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            var random = new Random();

            var apple = new Stock("AAPL", 150.00m);
            var microsoft = new Stock("MSFT", 310.00m);
            var tesla = new Stock("TSLA", 230.00m);

            var ticker = new TickerDisplay();

            var investorA = new Investor("Nowak");
            var investorB = new Investor("Lisa");
            var investorC = new Investor("Hofihasi");

            // ticker "abonnieren", sodass Meldung über Kursänderung ausgegeben wird
            apple.Attach(ticker);
            microsoft.Attach(ticker);
            tesla.Attach(ticker);

            // Investoren, welche apple "abonniert" haben
            apple.Attach(investorA);
            apple.Attach(investorB);

            // Investoren, welche microsoft "abonniert" haben
            microsoft.Attach(investorA);
            microsoft.Attach(investorC);

            // Investoren, welche tesla "abonniert" haben
            tesla.Attach(investorB);
            tesla.Attach(investorC);

            var stocks = new List<Stock> { apple, microsoft, tesla };

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
            var apple = new Stock("AAPL");

            var investorA = new Investor("Nowak");
            var investorB = new Investor("Anna");
            var ticker = new TickerDisplay();

            apple.Attach(ticker);
            apple.Attach(investorA);
            apple.Attach(investorB);

            apple.SetPrice(150.25m);

            apple.Detach(investorB);
            apple.SetPrice(170.00m);
            */
        }
    }
}