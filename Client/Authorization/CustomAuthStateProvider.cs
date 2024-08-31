using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace ClientServer.Client.Authorization;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
	private readonly ILocalStorageService _localStorageService;
	private readonly HttpClient _httpClient;

	public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
	{
		_localStorageService = localStorageService;
		_httpClient = httpClient;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		AuthenticationState state = null;
		ClaimsIdentity identity = new ClaimsIdentity(); //empty = not authorized
		try
		{
			var token = await _localStorageService.GetItemAsStringAsync("token");
			var expirationTimestamp = 0;
			IEnumerable<Claim> claims = null;

			_httpClient.DefaultRequestHeaders.Authorization = null;

			if ((string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token) || token == "" || token.Equals("")) is false)
			{
				(claims, expirationTimestamp) = ParseClaimsFromJwt(token);

				if (expirationTimestamp > DateTimeOffset.Now.ToUnixTimeSeconds() && claims is not null)
				{
					identity = new ClaimsIdentity(claims, "jwt");
					_httpClient.DefaultRequestHeaders.Authorization =
						new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
				}
			}
		}
		catch (Exception e)
		{
			await Console.Out.WriteLineAsync(e.ToString());
		}

		var user = new ClaimsPrincipal(identity);

		state = new AuthenticationState(user);

		NotifyAuthenticationStateChanged(Task.FromResult(state)); //notify specific components that something regarding user session has changed

		return state;
	}

	private static (IEnumerable<Claim>, int) ParseClaimsFromJwt(string jwt)
	{
		var payload = jwt.Split('.')[1];
		var jsonBytes = ParseBase64WithoutPadding(payload);
		var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
		var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
		//var expiration = keyValuePairs.Select(exp => exp.Value).FirstOrDefault();

		var expirationStr = keyValuePairs.FirstOrDefault(kvp => kvp.Key.Equals("exp")).Value.ToString();

		if (int.TryParse(expirationStr, out var expiration))
		{
			return (claims, expiration);
		}

		return (null, 0);
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
