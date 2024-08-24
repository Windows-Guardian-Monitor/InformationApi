using ClientServer.Client.Services.Contracts;

namespace ClientServer.Client.Services
{
    public class UserSessionService : IUserSessionService
	{
		private readonly ILocalStorageService _localStorageService;
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public UserSessionService(ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
		{
			_localStorageService = localStorageService;
			_authenticationStateProvider = authenticationStateProvider;
		}

		public async Task Logout()
		{
			const string token = "token";
			await _localStorageService.RemoveItemAsync(token);
			await _authenticationStateProvider.GetAuthenticationStateAsync();
		}

		public async Task Login(string jwtToken)
		{
			const string token = "token";
			await _localStorageService.SetItemAsync(token, jwtToken);
			await _authenticationStateProvider.GetAuthenticationStateAsync();
		}
	}
}
