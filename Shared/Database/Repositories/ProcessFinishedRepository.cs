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

		public List<ProcessFinishedEvent> GetByDate(CustomDate date)
		{
			var processFinishedEvents = new List<ProcessFinishedEvent>();

			var filteredEvents = _context.ProcessFinishedEvents.ToList().Where(p => IsWithinDate(p.Timestamp, date));

			return filteredEvents.ToList();
		}

		private static bool IsWithinDate(long timestamp, CustomDate date)
		{
			var d = timestamp.TimestampToDatetime();

			if (d.Day == date.Day && d.Month == date.Month && d.Year == date.Year)
			{
				return true;
			}

			return false;
		}
	}
}
