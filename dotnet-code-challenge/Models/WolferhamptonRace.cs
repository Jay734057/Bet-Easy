using System.Collections.Generic;

namespace dotnet_code_challenge.Models
{
    public class WolferhamptonRace
    {
        public RawData RawData { get; set; }

    }
    public class Tag
    {
        public string participant { get; set; }
        public string name { get; set; }
    }

    public class Selection
    {
        public string Id { get; set; }
        public double Price { get; set; }
        public Tag Tags { get; set; }
    }

    
    public class Market
    {
        public string Id { get; set; }
        public List<Selection> Selections { get; set; }
    }

    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class RawData
    {
        public List<Market> Markets { get; set; }
        public List<Participant> Participants { get; set; }
    }
}
