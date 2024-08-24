﻿using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;

namespace InformationHandlerApi.Database.Repositories
{
    public class ProgramRepository : IProgramRepository
	{
		private readonly DatabaseContext _context;

		public ProgramRepository(DatabaseContext context)
		{
			_context = context;
		}

		public bool Exists(string hash) => _context.Programs.Any(p => p.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase));
		public void InsertMany(IEnumerable<DbProgram> programs)
		{
			_context.Programs.AddRange(programs);
			_context.SaveChanges();
		}

		public void Insert(DbProgram program)
		{
			_context.Programs.Add(program);
			_context.SaveChanges();
		}

		public List<DbProgram> GetAll() => _context.Programs.ToList();
	}
}