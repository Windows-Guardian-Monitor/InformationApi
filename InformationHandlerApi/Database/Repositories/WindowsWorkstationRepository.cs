using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

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
                .FirstOrDefault(workstation =>
                workstation.Uuid.Equals(dbWindowsWorkstation.Uuid));

            try
            {
                if (workstation is null)
                {
                    var last = _databaseContext.Workstations.OrderBy(w => w.Id).LastOrDefault();

                    int wsId;

                    if (last is null)
                    {
                        wsId = 1;
                    }
                    else
                    {
                        wsId = last.Id++;
                    }

                    foreach (var disk in dbWindowsWorkstation.DisksInfo)
                    {
                        disk.WsId = wsId;
                    }

                    await _databaseContext.Workstations.AddAsync(dbWindowsWorkstation);
                    return;
                }

                var cpu = _databaseContext.Cpus.FirstOrDefault(c => c.CpuInfoId == workstation.Id);
                var disks = _databaseContext.Disks.Where(c => c.WsId == workstation.Id).ToArray();
                var os = _databaseContext.Systems.FirstOrDefault(c => c.OsInfoId == workstation.Id);
                var ram = _databaseContext.Rams.FirstOrDefault(c => c.RamInfoId == workstation.Id);

                var i = 0;
                foreach (var disk in dbWindowsWorkstation.DisksInfo)
                {
                    disk.WsId = workstation.Id;
                    disk.Id = disks[i].Id;
                    i++;
                }

                dbWindowsWorkstation.CpuInfo.CpuInfoId = cpu.CpuInfoId;
                dbWindowsWorkstation.RamInfo.RamInfoId = ram.RamInfoId;
                dbWindowsWorkstation.OsInfo.OsInfoId = os.OsInfoId;

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