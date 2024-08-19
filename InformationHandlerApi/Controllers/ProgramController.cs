using InformationHandlerApi.Business.Requests;
using InformationHandlerApi.Business.Responses;
using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InformationHandlerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProgramController : Controller
	{
		private readonly IProgramRepository _programRepository;

		public ProgramController(IProgramRepository programRepository)
		{
			_programRepository = programRepository;
		}

		[HttpPost("Send")]
		public ValueTask<ActionResult<StandardResponse>> PostPrograms([FromBody] byte[] serializedProgramList)
		{
			try
			{
				var programs = JsonSerializer.Deserialize<List<ProgramRequest>>(serializedProgramList);

				if (programs is null)
				{
					return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse
					{
						Code = System.Net.HttpStatusCode.InternalServerError,
						Message = "Could not obtain workstation info"
					});
				}

				foreach (ProgramRequest? program in programs)
                {
					if (_programRepository.Exists(program.Hash))
					{
						continue;
					}

					_programRepository.Insert(program);
				}

                return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse
				{
					Code = System.Net.HttpStatusCode.OK,
					Message = "OK"
				});
			}
			catch (Exception e)
			{
				return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse
				{
					Code = System.Net.HttpStatusCode.InternalServerError,
					Message = e.Message
				});
			}
		}

		[HttpGet(Name = "GetAll")]
		public List<DbProgram> Get() => _programRepository.GetAll();
	}
}
