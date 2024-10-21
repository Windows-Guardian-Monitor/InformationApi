using ClientServer.Shared.Database.Repositories.Performance;
using ClientServer.Shared.Models;
using ClientServer.Shared.Reponses;
using ClientServer.Shared.Reponses.Performances;
using ClientServer.Shared.Requests.Performances;
using InformationHandlerApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InformationHandlerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PerformanceController : Controller
	{
		private readonly CpuPerformanceRepository _cpuPerformanceRepository;
		private readonly RamPerformanceRepository _ramPerformanceRepository;
		private readonly PerformanceSeparatorService _performanceSeparatorService;

		public PerformanceController(
			CpuPerformanceRepository performanceRepository, 
			RamPerformanceRepository ramPerformanceRepository, 
			PerformanceSeparatorService performanceSeparatorService)
		{
			_cpuPerformanceRepository = performanceRepository;
			_ramPerformanceRepository = ramPerformanceRepository;
			_performanceSeparatorService = performanceSeparatorService;
		}

		[HttpPost("SendCpuPerformanceInformation")]
		public StandardResponse SendPerformanceInformation([FromBody] string cpuPerformanceModelJson)
		{
			try
			{
				var cpuPerformanceModel = JsonSerializer.Deserialize<CpuPerformanceModel>(cpuPerformanceModelJson);
				_cpuPerformanceRepository.Insert(cpuPerformanceModel);
				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("SendRamPerformanceInformation")]
		public StandardResponse SendRamformanceInformation([FromBody] string performanceModelJson)
		{
			try
			{
				var ramPerformanceModel = JsonSerializer.Deserialize<RamPerformanceModel>(performanceModelJson);
				_ramPerformanceRepository.Insert(ramPerformanceModel);
				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("GetPerformanceInformation")]
		public PerformanceResponse GetPerformanceInformation(PerformanceRequest performanceRequest)
		{
			try
			{
				var cpuPerformances = _cpuPerformanceRepository.GetLastByMachineName(performanceRequest.MachineName);

				var ramPerformances = _ramPerformanceRepository.GetLastByMachineName(performanceRequest.MachineName);

				return new PerformanceResponse(cpuPerformances, ramPerformances, string.Empty, true, System.Net.HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new PerformanceResponse(null, null, e.Message, false, System.Net.HttpStatusCode.OK);
			}
		}

		[HttpPost("GetSpecifcPerformanceInformation")]
		public OneDayPerformanceResponse GetPerformanceInformation([FromBody] string performanceRequestJson)
		{
			try
			{
				var performanceRequest = JsonSerializer.Deserialize<OneDayPerformanceRequest>(performanceRequestJson);

				var cpuPerformances = _cpuPerformanceRepository.GetByMachineAndDate(performanceRequest.MachineName, performanceRequest.CustomDate);

				var ramPerformances = _ramPerformanceRepository.GetByMachineAndDate(performanceRequest.MachineName, performanceRequest.CustomDate);

				var cpuUsageMedia = _performanceSeparatorService.OrganizePerformanceByTimeOfDay(cpuPerformances);
				
				var ramUsageMedia = _performanceSeparatorService.OrganizePerformanceByTimeOfDay(ramPerformances);

				return new OneDayPerformanceResponse(cpuUsageMedia, ramUsageMedia, string.Empty, true, System.Net.HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new OneDayPerformanceResponse(null, null, e.Message, false, System.Net.HttpStatusCode.OK);
			}
		}
	}
}