using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoOutings.Library
{
    public class Outing
    {
        public Event EventType { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime EventDate { get; set; }
        public decimal CostPerPerson { get; }
        public decimal TotalCost { get; set; }
        public Outing(Event eventType, int numberOfPeople, DateTime date, decimal totalCost)
        {
            EventType = eventType;
            NumberOfPeople = numberOfPeople;
            EventDate = date;
            TotalCost = totalCost;
            CostPerPerson = TotalCost / NumberOfPeople;
        }
       public enum Event
        {
            Golf = 1,
            Bowling,
            AmusementPark,
            Concert
        }
    }
}
