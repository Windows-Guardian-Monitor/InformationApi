using ClientServer.Shared.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientServer.Shared.Database.Repositories
{
	public class WorkstationRulesRepository
	{
		private readonly DatabaseContext _context;

		public WorkstationRulesRepository(DatabaseContext context)
		{
			_context = context;
		}

		public void Insert(DbWorkstationSpecificRule dbWorkstationSpecificRule)
		{
			_context.WsRules.Add(dbWorkstationSpecificRule);
			_context.SaveChanges();
		}

		//var w = _context.WsRules
		//	.Include(w => w.Programs)
		//	.Include(w => w.Workstations)
		//	.ToList();

		public void DeleteById(int id)
		{
			var dbWorkstation = _context.WsRules.FirstOrDefault(w => w.WorkstationSpecificRuleId == id);

			if (dbWorkstation is null)
			{
				throw new Exception("Não foi possível apagar a regra para atualização");
			}

			_context.WsRules.Remove(dbWorkstation);

			_context.SaveChanges();
		}

		public List<DbWorkstationSpecificRule> GetAll() => 
			_context.WsRules
			.Include(w => w.Programs)
			.Include(w => w.Workstations)
			.ToList();

		public DbWorkstationSpecificRule GetById(int id) => _context.WsRules
			.Include(w => w.Programs)
			.Include(w => w.Workstations)
			.FirstOrDefault(w => w.WorkstationSpecificRuleId == id);
	}
}
