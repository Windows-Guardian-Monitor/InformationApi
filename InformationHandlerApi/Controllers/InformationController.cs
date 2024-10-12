using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database.Models;
using ClientServer.Shared.DataTransferObjects;
using ClientServer.Shared.Reponses;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

		//[HttpGet(Name = "GetInformation")]
		//public object Get()
		//{

		//	return new { Data = "sample" };
		//}

		[HttpPost("Workstation")]
		public async ValueTask<ActionResult<StandardResponse>> PostWs([FromBody] byte[] windowsWorkstationBytes)
		{
			try
			{
				var windowsWorkstation = JsonSerializer.Deserialize<DbWindowsWorkstation>(windowsWorkstationBytes);

				if (windowsWorkstation is null)
				{
					return new StandardResponse("Could not obtain workstation info", false, HttpStatusCode.InternalServerError);
				}

				await _windowsWorkstationRepository.Upsert(windowsWorkstation);

				return new StandardResponse("OK", true, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new StandardResponse(e.Message, false, HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost("GetSpecificWorkstation")]
		public ActionResult<WorkstationResponse> GetWorkstation([FromBody] string strId)
		{
			try
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

				if (dbWorkstation is null)
				{
					return WorkstationResponse.Create(null, StandardResponse.CreateBadRequest("Máquina não encontrada")); 
				}

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

				var wsItem = new WorkstationItem(
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

				return WorkstationResponse.Create(wsItem, StandardResponse.CreateOkResponse());
			}
			catch (Exception e)
			{
				return WorkstationResponse.Create(null, StandardResponse.CreateInternalServerErrorResponse(e.Message));
			}
		}

		[HttpGet("GetAllWorkstations")]
		public AllWorkstationsResponse GetSimpleWorkstations()
		{
			try
			{
				if (_windowsWorkstationRepository.Count() is 0)
				{
					return new AllWorkstationsResponse(new List<SimpleWorkstationItem>(), "Não há máquinas disponíveis", false, HttpStatusCode.OK);
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

				return new AllWorkstationsResponse(items, string.Empty, true, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new AllWorkstationsResponse(null, e.Message, false, HttpStatusCode.OK);
			}
		}
	}
}
