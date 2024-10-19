using ClientServer.Shared.Models;
using ClientServer.Shared.Requests.Events;

namespace ClientServer.Shared.Database.Repositories.Performance
{
	public class RamPerformanceRepository
    {
		private readonly DatabaseContext _context;

		public RamPerformanceRepository(DatabaseContext context)
		{
			_context = context;
		}

		public void Insert(RamPerformanceModel performanceModel)
		{
            var dbPerf =
                _context.RamPerformanceMonitor.ToList().LastOrDefault(c => c.MachineName.Equals(performanceModel.MachineName, StringComparison.OrdinalIgnoreCase));

            if (dbPerf is not null)
            {
                if (dbPerf.RamUsagePercentage.Equals(performanceModel.RamUsagePercentage, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            _context.RamPerformanceMonitor.Add(performanceModel);
			_context.SaveChanges();
		}

		public List<RamPerformanceModel> GetByMachineAndDate(string machineName, CustomDate customDate)
		{
			var performances = _context.RamPerformanceMonitor.ToList().Where(p => IsWithinMachineNameAndDate(p, machineName, customDate));

			return performances.ToList();
		}

		public RamPerformanceModel GetLastByMachineName(string machineName)
		{
			var cpuPerf = _context.RamPerformanceMonitor.ToList().Where(p => IsWithinMachineName(p, machineName)).LastOrDefault();

			if (cpuPerf is not null)
			{
				return cpuPerf;
			}

			throw new Exception("Houve um erro ao buscar o desempenho mais recente");
		}

		private static bool IsWithinMachineName(RamPerformanceModel performanceModel, string machineName)
		{
			var isSearchedMachine = performanceModel.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase);

			if (isSearchedMachine)
			{
				return true;
			}

			return false;
		}

		private static bool IsWithinMachineNameAndDate(RamPerformanceModel performanceModel, string machineName, CustomDate customDate)
		{
			var isWithinSelectedDay =
				performanceModel.DateTime.Day == customDate.Day &&
				performanceModel.DateTime.Month == customDate.Month &&
				performanceModel.DateTime.Year == customDate.Year;

			var isSearchedMachine = performanceModel.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase);

			if (isSearchedMachine)
			{
				return true;
			}

			return false;
		}

	}
}
