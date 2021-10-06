using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoOutings.Library
{
    public class OutingRepo
    {
        List<Outing> _listOfOutings = new List<Outing>();
        public void AddToList(Outing outing)
        {
            _listOfOutings.Add(outing);
        }
        public List<Outing> GetList()
        {
            return _listOfOutings;
        }
        public List<Outing> GetOutingsByEvent(int eventType)
        {
            List<Outing> _output = new List<Outing>();
            foreach (Outing outing in _listOfOutings)
            {
                if((int)outing.EventType == eventType)
                {
                    _output.Add(outing);
                }
            }
            return _output;
        }
        public decimal GetTotalCost(List<Outing> _outings)
        {
            decimal cost = 0;
            foreach (Outing outing in _outings)
            {
                cost += outing.TotalCost;
            }
            return cost;
        }
    }
}
