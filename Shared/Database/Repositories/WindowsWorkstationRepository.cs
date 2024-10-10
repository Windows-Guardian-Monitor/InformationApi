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

		public int Count() => _databaseContext.Workstations.Count();

		public List<DbWindowsWorkstation> SelectWorkstations() => _databaseContext.Workstations.Include(x => x.DisksInfo).ToList();

		public DbWindowsWorkstation SelectWorkstationsAndAttributesById(int id) => _databaseContext.Workstations
				.Include(x => x.CpuInfo)
				.Include(x => x.OsInfo)
				.Include(x => x.RamInfo)
				.Include(x => x.DisksInfo)
			.FirstOrDefault(ws => ws.Id == id);

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
					await _databaseContext.Workstations.AddAsync(dbWindowsWorkstation);
					return;
				}

				var i = 0;

				var dbDiskCount = dbWindowsWorkstation.DisksInfo.Count();
				var requestDiskCount = workstation.DisksInfo.Count();

				if (dbDiskCount == requestDiskCount)
				{
					var diksArray = workstation.DisksInfo.ToArray();
					foreach (var disk in dbWindowsWorkstation.DisksInfo)
					{
						disk.WorkstationId = workstation.Id;
						disk.Id = diksArray[i].Id;
						i++;
					}
				}
                else
                {
					if (dbDiskCount < requestDiskCount)
					{
						dbWindowsWorkstation.DisksInfo = new List<DbDiskInfo>();

						foreach (var item in workstation.DisksInfo)
						{
							((List<DbDiskInfo>)(dbWindowsWorkstation.DisksInfo)).Add(item);
						}
					}
                }

				dbWindowsWorkstation.CpuInfo.CpuInfoId = workstation.CpuInfo.CpuInfoId;
				dbWindowsWorkstation.OsInfo.OsInfoId = workstation.OsInfo.OsInfoId;
				dbWindowsWorkstation.RamInfo.RamInfoId = workstation.RamInfo.RamInfoId;

				workstation.DisksInfo = dbWindowsWorkstation.DisksInfo;
				workstation.CpuInfo = dbWindowsWorkstation.CpuInfo;
				workstation.RamInfo = dbWindowsWorkstation.RamInfo;
				workstation.OsInfo = dbWindowsWorkstation.OsInfo;

				workstation.HostName = dbWindowsWorkstation.HostName;

				_databaseContext.ChangeTracker.Clear();
				_databaseContext.Workstations.Update(workstation);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				try
				{
					await _databaseContext.SaveChangesAsync();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}

		}
	}
}