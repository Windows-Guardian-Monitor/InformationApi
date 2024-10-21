using ClientServer.Client.Models;

namespace ClientServer.Client.Services.Contracts
{
    public interface IUserSessionService
    {
        Task Login(UserSessionInformation userSessionInformation);
        Task Logout();
        Task<UserSessionInformation> GetSessionInformationAsync();
	}
}