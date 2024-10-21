using ClientServer.Shared.Database.Models;
using ClientServer.Shared.Requests.Contracts;

namespace ClientServer.Shared.Requests
{
	public class CreateRuleRequest : IAddOrUpdateRuleRequest
	{
		public CreateRuleRequest(string ruleName, List<DbRuleProgram> selectedPrograms)
		{
			RuleName = ruleName;
			SelectedPrograms = selectedPrograms;
		}

		public string RuleName { get; set; }
		public List<DbRuleProgram> SelectedPrograms { get; set; }
	}
}
