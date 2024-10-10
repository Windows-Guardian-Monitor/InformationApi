using InformationHandlerApi.Business.Requests.Events;
using InformationHandlerApi.Database;
using InformationHandlerApi.Extensions;
using System.Linq;

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

		public List<ProcessFinishedEvent> GetByDate(DateTime date)
		{
			var processFinishedEvents = new List<ProcessFinishedEvent>();

			var filteredEvents = _context.ProcessFinishedEvents.Where(p => IsWithinFilter(p.Timestamp, date));

			return filteredEvents.ToList();
		}

		private bool IsWithinFilter(long timestamp, DateTime date)
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
