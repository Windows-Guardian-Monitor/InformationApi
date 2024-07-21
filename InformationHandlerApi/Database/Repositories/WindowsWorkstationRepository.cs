using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;

namespace InformationHandlerApi.Database.Repositories
{
    public class WindowsWorkstationRepository : IWindowsWorkstationRepository
    {
        private readonly DatabaseContext _databaseContext;

        public WindowsWorkstationRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async ValueTask Upsert(DbWindowsWorkstation dbWindowsWorkstation)
        {
            dbWindowsWorkstation.Uuid = dbWindowsWorkstation.Uuid.ToUpper();

            var workstation = _databaseContext.Workstations.FirstOrDefault(workstation =>
                workstation.Uuid.Equals(dbWindowsWorkstation.Uuid));

            try
            {
                if (workstation is null)
                {
                    await _databaseContext.Workstations.AddAsync(dbWindowsWorkstation);
                    return;
                }

                workstation.DisksInfo = dbWindowsWorkstation.DisksInfo;
                workstation.CpuInfo = dbWindowsWorkstation.CpuInfo;
                workstation.RamInfo = dbWindowsWorkstation.RamInfo;
                workstation.OsInfo = dbWindowsWorkstation.OsInfo;

                _databaseContext.Workstations.Update(workstation);
            }
            finally
            {
                await _databaseContext.SaveChangesAsync();
            }

        }
    }
}