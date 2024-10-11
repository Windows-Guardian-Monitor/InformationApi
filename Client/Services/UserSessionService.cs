using ClientServer.Client.Authorization;
using ClientServer.Client.Models;
using ClientServer.Client.Services.Contracts;

namespace ClientServer.Client.Services
{
	public class UserSessionService : IUserSessionService
	{
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public UserSessionService(AuthenticationStateProvider authenticationStateProvider)
		{
			_authenticationStateProvider = authenticationStateProvider;
		}

		public async Task Logout() => await ((CustomAuthStateProvider)_authenticationStateProvider).RemoveAuthenticationState();

		public async Task Login(UserSessionInformation userSessionInformation) => await ((CustomAuthStateProvider)_authenticationStateProvider).SaveAuthenticationState(userSessionInformation);

		public async Task<UserSessionInformation> GetSessionInformationAsync() => await ((CustomAuthStateProvider)_authenticationStateProvider).GetSessionInformation();
	}
}
