using dotnet_code_challenge.Helper;
using dotnet_code_challenge.Models;
using System;
using System.Linq;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Fclp.FluentCommandLineParser<Args>();
            parser.Setup(x => x.CaulfieldRace).As('c', "Caulfield").SetDefault(false);
            parser.Setup(x => x.WolferhamptonRace).As('w', "Wolferhampton").SetDefault(false);

            var result = parser.Parse(args);

            if (result.EmptyArgs)
            {
                Console.WriteLine($"Usage: -[c]Caulfield true|false -w Wolferhampton true|false ");
                Environment.Exit(1);
            }

            if (parser.Object.CaulfieldRace)
            {
                Console.WriteLine("======================================================");
                Console.WriteLine("Caulfield Race Price Order");
                Console.WriteLine("======================================================");

                var doc = XmlParser<CaulfieldRace>.Parse("FeedData/Caulfield_Race1.xml");

                foreach (var race in doc.Races.Race)
                {
                    Console.WriteLine($"Race {race.Number}:");
                    var horsePrices = race.Prices.Price.Horses.Horse.OrderBy(x => double.Parse(x.Price));
                    var horses = race.Horses.Horse;
                    foreach (var horsePrice in horsePrices)
                    {
                        var horse = horses.Single(x => x.Number == horsePrice._Number);
                        Console.WriteLine($"Horse {horse.Number} - {horse.Name} - Price: {horsePrice.Price}");
                    }
                }

                Console.WriteLine();
            }


            if (parser.Object.WolferhamptonRace)
            {
                Console.WriteLine("======================================================");
                Console.WriteLine("Wolferhampton Race Price Order");
                Console.WriteLine("======================================================");

                var doc = JsonParser<WolferhamptonRace>.Parse("FeedData/Wolferhampton_Race1.json");

                foreach (var market in doc.RawData.Markets)
                {
                    Console.WriteLine($"Market {market.Id}:");
                    var selectionPrices = market.Selections.OrderBy(x => x.Price);
                    var participants = doc.RawData.Participants;

                    foreach (var selectionPrice in selectionPrices)
                    {
                        var participant = participants.Single(x => x.Id == Int32.Parse(selectionPrice.Tags.participant));
                        Console.WriteLine($"Participant {participant.Id} - {participant.Name} - Price: {selectionPrice.Price}");
                    }
                }
                Console.WriteLine();

            }

            Console.WriteLine("Press any key to exit . . .");
            Console.ReadKey();
        }
    }
}
