﻿using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database.Models.Authentication;

namespace ClientServer.Shared.Database.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DatabaseContext _context;

		public UserRepository(DatabaseContext context)
		{
			_context = context;
		}

		public void Insert(DbUser dbUser)
		{
			_context.Users.Add(dbUser);
			_context.SaveChanges();
		}

		public bool ExistsByUserName(string userName) => _context.Users.Any(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
		public bool ExistsByEmail(string email) => _context.Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

		public DbUser GetUserByUserName(string userName) => _context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

		public DbUser GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

		public DbUser GetByUserId(int id) => _context.Users.FirstOrDefault(u => u.Id == id);

		public List<DbUser> GetAll() => _context.Users.ToList();


		public void SetUserAlreadyLoggedIn(DbUser dbUser)
		{
			dbUser.HasLoggedIn = true;
			_context.Users.Update(dbUser);
			_context.SaveChanges(true);
		}

		public void Update(DbUser dbUser)
		{
			_context.Users.Update(dbUser);
			_context.SaveChanges(true);
		}

		public void Delete(int id)
		{
			var user = _context.Users.FirstOrDefault(u => u.Id ==id);
			_context.Users.Remove(user);
			_context.SaveChanges();
		}
	}
}
