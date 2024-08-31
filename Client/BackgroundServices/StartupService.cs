using ClientServer.Shared.DataTransferObjects.Authentication;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClientServer.Client.BackgroundServices
{
	public class StartupService
	{
		private readonly HttpClient _httpClient;

		public StartupService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task SetUpApplication()
		{
			try
			{
				const string endpoint = "Auth/Register";

				var url = $"{InformationHandler.GetUrl()}{endpoint}";

				var userDto = new UserDto()
				{
					UserName = "admin",
					Password = "admin"
				};

				await _httpClient.PostAsJsonAsync(url, userDto);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
