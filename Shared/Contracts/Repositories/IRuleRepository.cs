using InformationHandlerApi.Database.Models;

namespace InformationHandlerApi.Contracts.Repositories
{
    public interface IRuleRepository
    {
        void Insert(DbRule dbRule);
        List<DbRule> GetAll();
        void DeleteById(int id);
	}
}