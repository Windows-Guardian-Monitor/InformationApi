using ClientServer.Client.Operators;
using ClientServer.Client.Operators.Contracts;
using ClientServer.Client.Services;
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


			await builder.Build().RunAsync();
        }
    }
}
