using ClientServer.Shared.Database.Models;

namespace ClientServer.Shared.Contracts.Repositories
{
	public interface IWindowsWorkstationRepository
	{
		ValueTask Upsert(DbWindowsWorkstation dbWindowsWorkstation);

		DbWindowsWorkstation SelectWorkstationsAndAttributesById(int id);

		public List<DbWindowsWorkstation> SelectWorkstations();

		int Count();
	}
}