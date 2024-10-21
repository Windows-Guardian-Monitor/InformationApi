using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database;
using ClientServer.Shared.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientServer.Shared.Database.Repositories
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

		public void DeleteById(int id)
		{
			var rule = new DbRule { RuleId = id };
			_context.Rules.Attach(rule);
			_context.Rules.Remove(rule);
			_context.SaveChanges();
		}

		public DbRule GetById(int id)
		{
			return _context.Rules.Include(r => r.Programs).FirstOrDefault(r => r.RuleId == id);
		}
	}
}
