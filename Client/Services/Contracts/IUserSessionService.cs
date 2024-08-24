namespace ClientServer.Client.Services.Contracts
{
    public interface IUserSessionService
    {
        Task Login(string jwtToken);
        Task Logout();
    }
}