using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_5_SystemProgramming_CriticalSections
{
	internal class Bus
	{
        public string BusName { get; set; }
        public int MaxCountPassengers { get; }
        public int CurrenCountPassengers { get; set; }

        public Bus(string _busName = "Автобус №175", int _maxCount = 25)
        {
            BusName = _busName;
            MaxCountPassengers = _maxCount;
        }
    }
}
