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

            var doc2 = JsonParser<WolferhamptonRace>.Parse("FeedData/Wolferhampton_Race1.json");

            foreach (var market in doc2.RawData.Markets)
            {
                Console.WriteLine($"Market {market.Id}:");
                var selectionPrices = market.Selections.OrderBy(x => x.Price);
                var participants = doc2.RawData.Participants;

                foreach (var selectionPrice in selectionPrices)
                {
                    var participant = participants.Single(x => x.Id == Int32.Parse(selectionPrice.Tags.participant));
                    Console.WriteLine($"Participant {participant.Id} - {participant.Name} - Price: {selectionPrice.Price}");
                }
            }
        }
    }
}
