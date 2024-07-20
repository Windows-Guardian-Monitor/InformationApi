using InformationHandlerApi.Business.Responses;
using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database;
using InformationHandlerApi.Database.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async ValueTask<ActionResult<StandardResponse>> PostTodoItem(DbWindowsWorkstation windowsWorkstation)
        {
            try
            {
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
    }
}
