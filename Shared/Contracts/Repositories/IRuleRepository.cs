using ClientServer.Shared.Database.Models;

namespace ClientServer.Shared.Contracts.Repositories
{
	public interface IRuleRepository
	{
		void Insert(DbRule dbRule);
		List<DbRule> GetAll();
		void DeleteById(int id);
		DbRule GetById(int id);
	}
}