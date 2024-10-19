using ClientServer.Shared.Models;
using ClientServer.Shared.Requests.Events;

namespace ClientServer.Shared.Database.Repositories.Performance
{
	public class CpuPerformanceRepository
	{
		private readonly DatabaseContext _context;

		public CpuPerformanceRepository(DatabaseContext context)
		{
			_context = context;
		}

		public void Insert(CpuPerformanceModel performanceModel)
		{
			var dbPerf =
				_context.CpuPerformanceMonitor.ToList().LastOrDefault(c => c.MachineName.Equals(performanceModel.MachineName, StringComparison.OrdinalIgnoreCase));

			if (dbPerf is not null)
			{
				if (dbPerf.CpuUsagePercentage.Equals(performanceModel.CpuUsagePercentage, StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
			}

			_context.CpuPerformanceMonitor.Add(performanceModel);
			_context.SaveChanges();
		}

		public List<CpuPerformanceModel> GetByMachineAndDate(string machineName)
		{
			var performances = _context.CpuPerformanceMonitor.ToList().Where(p => IsWithinMachineNameAndDate(p, machineName));

			return performances.ToList();
		}

		public CpuPerformanceModel GetLast(string machineName)
		{
			var cpuPerf = _context.CpuPerformanceMonitor.ToList().Where(p => IsWithinMachineNameAndDate(p, machineName)).LastOrDefault();

			if (cpuPerf is not null)
			{
				return cpuPerf;
			}

			throw new Exception("Houve um erro ao buscar o desempenho mais recente");
		}

		private static bool IsWithinMachineNameAndDate(CpuPerformanceModel performanceModel, string machineName)
		{
			//var isWithinSelectedDay =
			//    performanceModel.DateTime.Day == customDate.Day &&
			//    performanceModel.DateTime.Month == customDate.Month &&
			//    performanceModel.DateTime.Year == customDate.Year;

			var isSearchedMachine = performanceModel.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase);

			if (isSearchedMachine)
			{
				return true;
			}

			return false;
		}
	}
}
