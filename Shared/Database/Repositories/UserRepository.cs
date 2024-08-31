using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database.Models.Authentication;
using InformationHandlerApi.Database;

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

		public bool Exists(string userName) => _context.Users.Any(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

		public DbUser GetUser(string userName) => _context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));


	}
}
