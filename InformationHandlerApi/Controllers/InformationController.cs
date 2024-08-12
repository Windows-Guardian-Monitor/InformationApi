using ClientServer.Shared.DataTransferObjects;
using InformationHandlerApi.Business.Responses;
using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InformationHandlerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class InformationController : Controller
	{
		private readonly IWindowsWorkstationRepository _windowsWorkstationRepository;

		public InformationController(IWindowsWorkstationRepository windowsWorkstationRepository)
		{
			_windowsWorkstationRepository = windowsWorkstationRepository;
		}

		[HttpGet(Name = "GetInformation")]
		public object Get()
		{

			return new { Data = "sample" };
		}

		[HttpPost("Workstation")]
		public async ValueTask<ActionResult<StandardResponse>> PostWs([FromBody] byte[] windowsWorkstationBytes)
		{
			try
			{
				var windowsWorkstation = JsonSerializer.Deserialize<DbWindowsWorkstation>(windowsWorkstationBytes);

				if (windowsWorkstation is null)
				{
					return new StandardResponse
					{
						Code = System.Net.HttpStatusCode.InternalServerError,
						Message = "Could not obtain workstation info"
					};
				}

				await _windowsWorkstationRepository.Upsert(windowsWorkstation);

				return new StandardResponse
				{
					Code = System.Net.HttpStatusCode.OK,
					Message = "OK"
				};
			}
			catch (Exception e)
			{
				return new StandardResponse
				{
					Code = System.Net.HttpStatusCode.InternalServerError,
					Message = e.Message
				};
			}
		}

		[HttpPost("GetSpecificWorkstation")]
		public ActionResult<WorkstationItem> GetWorkstation([FromBody] string strId)
		{
			if (_windowsWorkstationRepository.Count() is 0)
			{
				return NotFound();
			}

			if (int.TryParse(strId, out var id) is false)
			{
				return BadRequest("Incorrect parameter type");
			}

			var dbWorkstation = _windowsWorkstationRepository.SelectWorkstationsAndAttributesById(id);

			var disks = new List<DiskItem>();
			foreach (var dbDisk in dbWorkstation.DisksInfo)
			{
				var disk = new DiskItem
				{
					DiskName = dbDisk.DiskName,
					DiskType = dbDisk.DiskType,
					TotalSize = dbDisk.TotalSize,
				};

				disks.Add(disk);

			}

			return new WorkstationItem(
				dbWorkstation.HostName,
				dbWorkstation.Uuid,
				dbWorkstation.CpuInfo.Description,
				dbWorkstation.CpuInfo.Name,
				dbWorkstation.CpuInfo.CpuManufacturer,
				dbWorkstation.CpuInfo.Architecture,
				dbWorkstation.RamInfo.TotalMemory,
				dbWorkstation.RamInfo.Speed,
				dbWorkstation.RamInfo.Manufacturer,
				dbWorkstation.OsInfo.Description,
				dbWorkstation.OsInfo.OsManufacturer,
				dbWorkstation.OsInfo.SerialNumber,
				disks);
		}

		[HttpGet("GetAllWorkstations")]
		public ActionResult<List<SimpleWorkstationItem>> GetSimpleWorkstations()
		{
			if (_windowsWorkstationRepository.Count() is 0)
			{
				return new List<SimpleWorkstationItem>();
			}

			var workstations = _windowsWorkstationRepository.SelectWorkstations();

			var items = new List<SimpleWorkstationItem>();

			foreach (var dbWorkstation in workstations)
			{
				var workstationItem = new SimpleWorkstationItem
				{
					HostName = dbWorkstation.HostName,
					Id = dbWorkstation.Id,
					Uuid = dbWorkstation.Uuid
				};

				items.Add(workstationItem);
			}

			return items;
		}
	}
}
