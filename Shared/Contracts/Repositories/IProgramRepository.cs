using InformationHandlerApi.Database.Models;

namespace InformationHandlerApi.Contracts.Repositories
{
    public interface IProgramRepository
    {
        bool Exists(string hash);
        void InsertMany(IEnumerable<DbProgram> programs);
        void Insert(DbProgram program);
        List<DbProgram> GetAll();
	}
}