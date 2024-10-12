using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database.Models;
using ClientServer.Shared.Reponses;
using InformationHandlerApi.Business.Requests;
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
				var programs = JsonSerializer.Deserialize<List<ProgramRequestItem>>(serializedProgramList);

				if (programs is null)
				{
					return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse("Could not obtain workstation info", false, System.Net.HttpStatusCode.InternalServerError));
				}

				foreach (ProgramRequestItem? program in programs)
				{
					if (_programRepository.Exists(program.Hash))
					{
						continue;
					}

					_programRepository.Insert(program);
				}

				return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse("OK", true, System.Net.HttpStatusCode.OK));
			}
			catch (Exception e)
			{
				return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse(e.Message, false, System.Net.HttpStatusCode.InternalServerError));
			}
		}

		[HttpGet(Name = "GetAll")]
		public List<DbProgram> Get() => _programRepository.GetAll();

		[HttpPost("GetPerHostname")]
		public PerHostnameProgramsResponse Post([FromBody] string hostName)
		{
			try
			{
				var programs = _programRepository.GetByHostname(hostName);

				return new PerHostnameProgramsResponse(programs, string.Empty, true, System.Net.HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new PerHostnameProgramsResponse(null, e.Message, false, System.Net.HttpStatusCode.OK);
			}
		}
	}
}
