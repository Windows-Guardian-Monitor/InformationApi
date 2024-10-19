using ClientServer.Shared.Database.Repositories.Performance;
using ClientServer.Shared.Models;
using ClientServer.Shared.Reponses;
using ClientServer.Shared.Reponses.Performances;
using ClientServer.Shared.Requests.Performances;
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

        public PerformanceController(CpuPerformanceRepository performanceRepository, RamPerformanceRepository ramPerformanceRepository)
        {
            _cpuPerformanceRepository = performanceRepository;
            _ramPerformanceRepository = ramPerformanceRepository;
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
        public StandardResponse SendRamformanceInformation([FromBody]string performanceModelJson)
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
                var cpuPerformances = _cpuPerformanceRepository.GetLast(performanceRequest.MachineName);

                var ramPerformances = _ramPerformanceRepository.GetLast(performanceRequest.MachineName);

                return new PerformanceResponse(cpuPerformances, ramPerformances, string.Empty, true, System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new PerformanceResponse(null, null, e.Message, false, System.Net.HttpStatusCode.OK);
            }
        }
    }
}