using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_5_SystemProgramming_CriticalSections
{
	internal class BusStop
	{
        public string BusStopName { get; set; }
        public int PassengersCount { get; set; }
        public BusStop(string _busStopName = "Конечная", int _passengers = 0)
        {
            PassengersCount = _passengers;
        }
    }
}
