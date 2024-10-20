using ClientServer.Shared.Models;

namespace InformationHandlerApi.Services
{
	public class PerformanceSeparatorService
	{
		private List<TimeOnly> _timesOfDay = new List<TimeOnly>()
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

		public List<int> OrganizePerformanceByTimeOfDay(List<CpuPerformanceModel> performances)
		{
			var performanceByTimeOfDay = new List<int>();

			for (int i = 0; i < _timesOfDay.Count; i++)
			{
				var a = _timesOfDay[i];

				var results =
					performances.Where(cpuPerformance => cpuPerformance.DateTime.Hour >= _timesOfDay[i].Hour && cpuPerformance.DateTime.Hour < (_timesOfDay[i].AddHours(1)).Hour);

				var rCount = results.Count();

				if (rCount <= 0)
				{
					performanceByTimeOfDay.Add(0);
					continue;
				}

				var totalUsageWithin = results.Sum(h => ConvertedUsage(h.CpuUsagePercentage));

				var media = totalUsageWithin / rCount;

				performanceByTimeOfDay.Add(media);
			}

			return performanceByTimeOfDay;
		}

		public List<int> OrganizePerformanceByTimeOfDay(List<RamPerformanceModel> performances)
		{
			var performanceByTimeOfDay = new List<int>();

			for (int i = 0; i < _timesOfDay.Count; i++)
			{
				var a = _timesOfDay[i];

				var results =
					performances.Where(cpuPerformance => cpuPerformance.DateTime.Hour >= _timesOfDay[i].Hour && cpuPerformance.DateTime.Hour < (_timesOfDay[i].AddHours(1)).Hour);

				var rCount = results.Count();

				if (rCount <= 0)
				{
					performanceByTimeOfDay.Add(0);
					continue;
				}

				var totalUsageWithin = results.Sum(h => ConvertedUsage(h.RamUsagePercentage));

				var media = totalUsageWithin / rCount;

				performanceByTimeOfDay.Add(media);
			}

			return performanceByTimeOfDay;
		}

		private static int ConvertedUsage(string usageStr)
		{
			if (int.TryParse(usageStr, out var usage))
			{
				return usage;
			}

			return 0;
		}
	}
}
