global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using ClientServer.Client.Authorization;
using ClientServer.Client.BackgroundServices;
using ClientServer.Client.Models;
using ClientServer.Client.Operators;
using ClientServer.Client.Operators.Contracts;
using ClientServer.Client.Services;
using ClientServer.Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ClientServer.Client
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddBlazorBootstrap();

            builder.Services.AddTransient<ICustomSnackbarOperator, CustomSnackbarOperator>();
            builder.Services.AddSingleton<PageNavigationHelper>();
            builder.Services.AddTransient<StartupService>();
			builder.Services.AddTransient<PieChartManager>();
			builder.Services.AddTransient<BarChartManager>();

            //Authorization
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredSessionStorage();
            //My creation
            builder.Services.AddScoped<IUserSessionService, UserSessionService>();
            //End

			await builder.Build().RunAsync();
        }
    }
}
