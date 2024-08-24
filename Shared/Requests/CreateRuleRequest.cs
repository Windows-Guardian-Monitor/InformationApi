using InformationHandlerApi.Database.Models;

namespace ClientServer.Shared.Requests
{
	public class CreateRuleRequest
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
