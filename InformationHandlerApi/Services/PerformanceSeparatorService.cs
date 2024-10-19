using ClientServer.Shared.Models;

namespace InformationHandlerApi.Services
{
	public class PerformanceSeparatorService
	{

		public List<string> OrganizePerformanceByTimeOfDay(List<CpuPerformanceModel> performances)
		{
			var performanceByTimeOfDay = new List<string>();

			var timesOfDay = new List<TimeOnly>()
			{
				new(0,0),
				new(1,0),
				new(2,0),
				new(3,0),
				new(4,0),
				new(5,0),
				new(6,0),
				new(7,0),
				new(8,0),
				new(9,0),
				new(10,0),
				new(11,0),
				new(12,0),
				new(13,0),
				new(14,0),
				new(15,0),
				new(16,0),
				new(17,0),
				new(18,0),
				new(19,0),
				new(20,0),
				new(21,0),
				new(22,0),
				new(23,0)
			};

			for (int i = 0; i < timesOfDay.Count; i++)
			{
				var a = timesOfDay[i];

				var breakBoth = false;

				//TODO CONITNUAR AQUI
				//Fazer uma lógica para obter a média daquele desempenho no momento, para então adicionar à lista, assim vai ficar melhor

				foreach (var cpuPerformance in performances)
                {
					if (cpuPerformance.DateTime.Hour >= timesOfDay[i].Hour && cpuPerformance.DateTime.Hour < (timesOfDay[i].AddHours(1)).Hour)
					{
						performanceByTimeOfDay.Add(cpuPerformance.CpuUsagePercentage);
						breakBoth = true;
						break;
					}
				}

				if (breakBoth is false)
				{
					performanceByTimeOfDay.Add("0");
				}
			}

			return performanceByTimeOfDay;
		}
	}
}
