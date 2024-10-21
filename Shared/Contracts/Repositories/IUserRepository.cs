using ClientServer.Shared.Database.Models.Authentication;

namespace ClientServer.Shared.Contracts.Repositories
{
    public interface IUserRepository
    {
        void Insert(DbUser dbUser);
        bool ExistsByUserName(string userName);
        DbUser GetUserByUserName(string userName);
        public DbUser GetUserByEmail(string email);
		void SetUserAlreadyLoggedIn(DbUser dbUser);
        bool ExistsByEmail(string email);
        void Update(DbUser dbUser);
        public List<DbUser> GetAll();
        public DbUser GetByUserId(int id);
        void Delete(int id);
	}
}