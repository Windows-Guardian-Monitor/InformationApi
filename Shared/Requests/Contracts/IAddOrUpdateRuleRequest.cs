using InformationHandlerApi.Database.Models;

namespace ClientServer.Shared.Requests.Contracts
{
	public interface IAddOrUpdateRuleRequest
	{
		public string RuleName { get; set; }
		public List<DbRuleProgram> SelectedPrograms { get; set; }
	}
}
