using Blazored.SessionStorage;
using ClientServer.Client.Models;
using System.Security.Claims;
using System.Text.Json;

namespace ClientServer.Client.Authorization;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
	private readonly ISessionStorageService _localStorageService;
	private readonly HttpClient _httpClient;

	private ClaimsPrincipal _anonymousClaims = new ClaimsPrincipal(new ClaimsIdentity());

	private const string token = "token";

	public CustomAuthStateProvider(ISessionStorageService localStorageService, HttpClient httpClient)
	{
		_localStorageService = localStorageService;
		_httpClient = httpClient;
	}

	public async Task<UserSessionInformation> GetSessionInformation() => await _localStorageService.GetItemAsync<UserSessionInformation>(token);

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var state = new AuthenticationState(_anonymousClaims);

		try
		{
			var sessionInfo = await _localStorageService.GetItemAsync<UserSessionInformation>(token);

			if (sessionInfo is null)
			{
				return state;
			}

			var claims = ParseClaimsFromJwt(sessionInfo.JwtToken);
			var expirationTimestamp = ParseExpirationFromJwt(sessionInfo.JwtToken);

			var identity = new ClaimsIdentity(); //empty = not authorized

			if (expirationTimestamp > DateTimeOffset.Now.ToUnixTimeSeconds() && claims is not null)
			{
				identity = new ClaimsIdentity(claims, "jwt");
			}

			var user = new ClaimsPrincipal(identity);

			state = new AuthenticationState(user);

			return state;
		}
		catch (Exception e)
		{
			return state;
		}
		finally
		{
			NotifyAuthenticationStateChanged(Task.FromResult(state)); //notify specific components that something regarding user session has changed
		}
	}

	public async ValueTask SaveAuthenticationState(UserSessionInformation userSessionInformation)
	{
		await _localStorageService.SetItemAsync(token, userSessionInformation);
		await GetAuthenticationStateAsync();
	}

	public async ValueTask RemoveAuthenticationState()
	{
		await _localStorageService.RemoveItemAsync(token);
		await GetAuthenticationStateAsync();
	}

	private static int ParseExpirationFromJwt(string jwt)
	{
		var payload = jwt.Split('.')[1];
		var jsonBytes = ParseBase64WithoutPadding(payload);
		var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

		var expirationStr = keyValuePairs.FirstOrDefault(kvp => kvp.Key.Equals("exp")).Value.ToString();

		if (int.TryParse(expirationStr, out var expiration))
		{
			return expiration;
		}

		return 0;
	}

	private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
	{
		var payload = jwt.Split('.')[1];
		var jsonBytes = ParseBase64WithoutPadding(payload);
		var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
		return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
	}

	private static byte[] ParseBase64WithoutPadding(string base64)
	{
		switch (base64.Length % 4)
		{
			case 2: base64 += "=="; break;
			case 3: base64 += "="; break;
		}
		return Convert.FromBase64String(base64);
	}
}
