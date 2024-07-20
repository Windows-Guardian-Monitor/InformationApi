using InformationHandlerApi.Database;
using InformationHandlerApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

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
