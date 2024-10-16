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
			_context.RamPerformanceMonitor.Add(performanceModel);
			_context.SaveChanges();
		}

		public List<RamPerformanceModel> GetByMachineAndDate(string machineName, CustomDate date)
		{
			var performances = _context.RamPerformanceMonitor.ToList().Where(p => IsWithinMachineNameAndDate(p, machineName, date));

			return performances.ToList();
		}

		private static bool IsWithinMachineNameAndDate(RamPerformanceModel performanceModel, string machineName, CustomDate customDate)
		{
			var isWithinSelectedDay =
				performanceModel.DateTime.Day == customDate.Day &&
				performanceModel.DateTime.Month == customDate.Month &&
				performanceModel.DateTime.Year == customDate.Year;

			var isSearchedMachine = performanceModel.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase);

			if (isSearchedMachine && isWithinSelectedDay)
			{
				return true;
			}

			return false;
		}
	}
}
