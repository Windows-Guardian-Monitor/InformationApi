using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Database.Models
{
	public class DbProgram
	{
		public DbProgram(string path, string name, string hash, string hostname)
		{
			Path = path;
			Name = name;
			Hash = hash;
			Hostname = hostname;
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

		[JsonPropertyName("Hostname")]
        public string Hostname { get; set; }
    }
}
