using ClientServer.Shared.Extensions;
using ClientServer.Shared.Requests.Events;

namespace ClientServer.Shared.Database.Repositories
{
	public class ProcessFinishedRepository
	{
		private readonly DatabaseContext _context;

		public ProcessFinishedRepository(DatabaseContext context)
		{
			_context = context;
		}

		public void InsertMany(ProcessFinishedEvent[] events)
		{
			_context.ProcessFinishedEvents.AddRange(events);
			_context.SaveChanges();
		}

		public List<ProcessFinishedEvent> GetByDateAndMachineName(CustomDate date, string machineName)
		{
			var processFinishedEvents = new List<ProcessFinishedEvent>();

			var filteredEvents = _context.ProcessFinishedEvents.ToList().Where(p => IsWithinDateAndMachineName(p, machineName, date));

			return filteredEvents.ToList();
		}

		private static bool IsWithinDateAndMachineName(ProcessFinishedEvent p, string machineName, CustomDate customDate)
		{
			var isSearchedMachine = p.MachineName.Equals(machineName, StringComparison.OrdinalIgnoreCase);

			var d = p.Timestamp.TimestampToDatetime();

			if (d.Day == customDate.Day && d.Month == customDate.Month && d.Year == customDate.Year)
			{
				return true;
			}

			return false;
		}
	}
}
