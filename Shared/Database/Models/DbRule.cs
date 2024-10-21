using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Database.Models
{
	public class DbRule
	{
		public DbRule(string name, List<DbRuleProgram> programs)
		{
			Programs = programs;
			Name = name;
		}

		public DbRule()
		{

		}

		[Key]
		[JsonPropertyName("RuleId")]
		public int RuleId { get; set; }

		[JsonPropertyName("Programs")]
		public List<DbRuleProgram> Programs { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }
	}
}
