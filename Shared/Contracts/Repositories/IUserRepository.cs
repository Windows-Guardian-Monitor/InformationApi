using ClientServer.Shared.Database.Models.Authentication;

namespace ClientServer.Shared.Contracts.Repositories
{
    public interface IUserRepository
    {
        void Insert(DbUser dbUser);
        bool Exists(string userName);
        DbUser GetUser(string userName);
	}
}