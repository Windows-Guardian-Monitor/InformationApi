using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
using Microsoft.EntityFrameworkCore;

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

            var workstation = _databaseContext.Workstations
                .Include(x => x.CpuInfo)
                .Include(x => x.OsInfo)
                .Include(x => x.RamInfo)
                .Include(x => x.DisksInfo)
                .FirstOrDefault(workstation => workstation.Uuid.Equals(dbWindowsWorkstation.Uuid));

            //var workstation = _databaseContext.Workstations
            //    .FirstOrDefault(workstation =>
            //    workstation.Uuid.Equals(dbWindowsWorkstation.Uuid));

            try
            {
                if (workstation is null)
                {
                    //var last = _databaseContext.Workstations.OrderBy(w => w.Id).LastOrDefault();

                    //int wsId;

                    //if (last is null)
                    //{
                    //    wsId = 1;
                    //}
                    //else
                    //{
                    //    wsId = last.Id++;
                    //}

                    //foreach (var disk in dbWindowsWorkstation.DisksInfo)
                    //{
                    //    disk.WsId = wsId;
                    //}

                    await _databaseContext.Workstations.AddAsync(dbWindowsWorkstation);
                    return;
                }

                var i = 0;
                var diksArray = workstation.DisksInfo.ToArray();
                foreach (var disk in dbWindowsWorkstation.DisksInfo)
                {
                    disk.WorkstationId = workstation.Id;
                    disk.Id = diksArray[i].Id;
                    i++;
                }

                dbWindowsWorkstation.CpuInfo.CpuInfoId = workstation.CpuInfo.CpuInfoId;
                dbWindowsWorkstation.OsInfo.OsInfoId = workstation.OsInfo.OsInfoId;
                dbWindowsWorkstation.RamInfo.RamInfoId = workstation.RamInfo.RamInfoId;

                workstation.DisksInfo = dbWindowsWorkstation.DisksInfo;
                workstation.CpuInfo = dbWindowsWorkstation.CpuInfo;
                workstation.RamInfo = dbWindowsWorkstation.RamInfo;
                workstation.OsInfo = dbWindowsWorkstation.OsInfo;

                _databaseContext.ChangeTracker.Clear();
                _databaseContext.Workstations.Update(workstation);
            }
            catch (Exception e)
            {

            }
            finally
            {
                try
                {
                    await _databaseContext.SaveChangesAsync();
                }
                catch (Exception e)
                {


                }
            }

        }
    }
}