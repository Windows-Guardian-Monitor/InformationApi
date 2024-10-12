﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Database.Models
{
	[Table("ProgramForWorkstationSpecificRule")]
	public class WorkstationSpecificDbRuleProgram
	{
		public WorkstationSpecificDbRuleProgram(string path, string name, string hash)
		{
			Path = path;
			Name = name;
			Hash = hash;
		}

		public WorkstationSpecificDbRuleProgram()
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



		[ForeignKey("WorkstationSpecificRuleId")]
		[JsonIgnore]
		[Column("WorkstationSpecificRuleId")]
		public DbWorkstationSpecificRule ForeignRule { get; set; }
	}

	[Table("WorkstationForWorkstationSpecificRule")]
	public class WorkstationForWorkstationRule
	{
		[Key]
		public int WorkstationId { get; set; }

		[JsonPropertyName("Hostname")]
		public string Hostname { get; set; }


		[ForeignKey("WorkstationSpecificRuleId")]
		[JsonIgnore]
		[Column("WorkstationSpecificRuleId")]
		public DbWorkstationSpecificRule ForeignRule { get; set; }
	}

	[Table("WorkstationSpecificRule")]
	public class DbWorkstationSpecificRule
	{
		public DbWorkstationSpecificRule(List<WorkstationSpecificDbRuleProgram> programs, List<WorkstationForWorkstationRule> workstations)
		{
			Programs = programs;
			Workstations = workstations;
		}

		public DbWorkstationSpecificRule()
		{

		}

		[Key]
		[JsonPropertyName("WorkstationSpecificRuleId")]
		public int WorkstationSpecificRuleId { get; set; }

		[JsonPropertyName("WorkstationSpecificRulePrograms")]
		public List<WorkstationSpecificDbRuleProgram> Programs { get; set; }

		[JsonPropertyName("WorkstationSpecificRuleWorkstations")]
		public List<WorkstationForWorkstationRule> Workstations { get; set; }
	}
}