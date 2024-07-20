using InformationHandlerApi.Database.Models;

namespace InformationHandlerApi.Contracts.Repositories
{
    public interface IWindowsWorkstationRepository
    {
        ValueTask Upsert(DbWindowsWorkstation dbWindowsWorkstation);
    }
}