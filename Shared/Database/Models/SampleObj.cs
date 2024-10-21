using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServer.Shared.Database.Models
{
	[Table("Samples")]
	public class SampleObj
	{
		public int Id { get; set; }
		public string Data { get; set; }
	}
}
