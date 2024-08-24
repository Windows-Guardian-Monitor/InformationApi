using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Database.Models
{
	public class DbProgram
	{
		public DbProgram(string path, string name, string hash)
		{
			Path = path;
			Name = name;
			Hash = hash;
		}

        public DbProgram()
        {
            
        }

        [Key]
		[JsonPropertyName("Id")]
		public int Id { get; set; }

		[JsonPropertyName("Path")]
		public string Path { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }

		[JsonPropertyName("Hash")]
		public string Hash { get; set; }
	}
}
