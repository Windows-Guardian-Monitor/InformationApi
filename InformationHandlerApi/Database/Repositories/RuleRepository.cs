using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationHandlerApi.Database.Repositories
{
	public class RuleRepository : IRuleRepository
	{
		private readonly DatabaseContext _context;

		public RuleRepository(DatabaseContext context)
		{
			_context = context;
		}

		public List<DbRule> GetAll()
		{
			return _context.Rules.Include(r => r.Programs).ToList();
		}

		public void Insert(DbRule dbRule)
		{
			_context.Rules.Add(dbRule);
			_context.SaveChanges();
		}
	}
}
