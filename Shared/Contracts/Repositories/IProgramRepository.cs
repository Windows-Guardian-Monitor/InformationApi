using ClientServer.Shared.Database.Models;

namespace ClientServer.Shared.Contracts.Repositories
{
	public interface IProgramRepository
	{
		bool Exists(string hash);
		void InsertMany(IEnumerable<DbProgram> programs);
		void Insert(DbProgram program);
		List<DbProgram> GetAll();
		List<DbProgram> GetByHostname(string hostName);
	}
}