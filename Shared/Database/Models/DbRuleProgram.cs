using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Database.Models
{
	public class DbRuleProgram
	{
		public DbRuleProgram(string path, string name, string hash)
		{
			Path = path;
			Name = name;
			Hash = hash;
		}

        public DbRuleProgram()
        {
            
        }

		[Key]
		[JsonPropertyName("Id")]
		public int ProgramId { get; set; }

		[JsonPropertyName("Path")]
		public string Path { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }

		[JsonPropertyName("Hash")]
		public string Hash { get; set; }
		

		[JsonPropertyName("Selected")]
		[NotMapped]
        public bool Selected { get; set; }



		[ForeignKey("RuleId")]
		[JsonIgnore]
        public DbRule ForeignRule { get; set; }
    }
}
