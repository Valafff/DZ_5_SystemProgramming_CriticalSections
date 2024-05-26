using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DZ_5_SystemProgramming_CriticalSections
{
	internal class Program
	{

		const int busCount = 5;
		const int passengerRespawn = 2500;
		const int busTimeout = 5000;
		const int busCapacity = 25;
		const int cyclesNumber = 24;
		static bool doWork = true;


		private static readonly object LockObj = new object();
		private static readonly object LockBusUp = new object();

		static void BusStopWork(ref BusStop _bs,  int _cycls)
		{
			Random random = new Random();
			int count = _cycls;
			do
			{
				lock (LockObj)
				{
					int temp = random.Next(1, 15);
					_bs.PassengersCount += temp;
					Console.WriteLine($"Номер цикла {cyclesNumber-count+1}");
					Console.WriteLine($"На остановку пришло {temp} пассажиров. Всего на остановке {_bs.PassengersCount} человек");
					count--;
				}
				Thread.Sleep(passengerRespawn);
			} while (count > 0);
			doWork = false;
		}


		static void BusWork(ref BusStop _targetBusStop, ref Bus _targetBus)
		{

			var tempBusStop = _targetBusStop;
			var tempBus = _targetBus;
			int tempCount;
			do
			{
				lock (LockBusUp)
				{
					if (tempBusStop.PassengersCount > 0)
					{
						Random random = new Random();
						Console.WriteLine($"Работает поток {Thread.CurrentThread.Name}");
						tempCount = random.Next(0, tempBusStop.PassengersCount > tempBus.MaxCountPassengers ? tempBus.MaxCountPassengers : tempBusStop.PassengersCount);
						Console.WriteLine($"Автобус номер {tempBus.BusName} приехал на остановку и забрал {tempCount} человек ({tempBusStop.PassengersCount} - {tempCount} = {tempBusStop.PassengersCount-tempCount})");
						tempBusStop.PassengersCount -= tempCount;
						tempBus.CurrenCountPassengers += tempCount;
					}
					tempBus.CurrenCountPassengers = 0;
					Thread.Sleep(busTimeout);
				}

			} while (doWork);
		}


		static void Main(string[] args)
		{
			BusStop busStop = new BusStop();
			Thread busStopWorkThread = new Thread(() => BusStopWork(ref busStop, cyclesNumber));
			busStopWorkThread.Start();


			for (int i = 0; i < busCount; i++)
			{
				Bus bus = new Bus($"Автобус №175-{i + 1}", busCapacity);
				Thread tempThread = new Thread(() => BusWork(ref busStop, ref bus));
				tempThread.Name = $"Поток {i + 1}";
				tempThread.Start();
			}
		}
	}
}
