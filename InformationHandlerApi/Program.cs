using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database;
using InformationHandlerApi.Database.Repositories;
using InformationHandlerApi.Services;
using Microsoft.EntityFrameworkCore;

namespace InformationHandlerApi
{
    public class Program
    {
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var conn = builder.Configuration.GetConnectionString("ConnectionString");
            builder.Services.AddMySql<DatabaseContext>(conn, ServerVersion.AutoDetect(conn));
            //builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(ServerVersion.AutoDetect(conn)));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<DatabaseHandlerService>();

            builder.Services.AddTransient<IWindowsWorkstationRepository, WindowsWorkstationRepository>();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            app.UseCors(corsBuilder => corsBuilder.AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
