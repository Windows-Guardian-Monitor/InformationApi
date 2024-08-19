using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Database.Models
{
	public class DbRule
	{
		public DbRule(List<DbRuleProgram> programs)
		{
			Programs = programs;
		}

        public DbRule()
        {
            
        }

        [Key]
		[JsonIgnore]
		public int Id { get; set; }

		[JsonPropertyName("Programs")]
		public List<DbRuleProgram> Programs { get; set; }
	}
}
