using ClientServer.Shared.Database.Models;
using ClientServer.Shared.DataTransferObjects;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests.Rules
{

	public class CreateWsSpecificRuleRequest
	{
		[JsonPropertyName("RuleName")]
        public string RuleName { get; set; }

        [JsonPropertyName("Workstations")]
        public List<SimpleWorkstationItem> Workstations { get; set; }

		[JsonPropertyName("Programs")]
		public List<DbRuleProgram> Programs { get; set; }
    }
}
