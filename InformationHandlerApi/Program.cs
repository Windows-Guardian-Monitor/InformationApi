using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database;
using ClientServer.Shared.Database.Repositories;
using ClientServer.Shared.Database.Repositories.Performance;
using ClientServer.Shared.Database.Repositories.Programs;
using InformationHandlerApi.Contracts;
using InformationHandlerApi.Services;
using Microsoft.EntityFrameworkCore;

namespace InformationHandlerApi
{
	public class Program
    {
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var conn = builder.Configuration.GetConnectionString("ConnectionString");
            builder.Services.AddMySql<DatabaseContext>(conn, ServerVersion.AutoDetect(conn), options => options.EnableStringComparisonTranslations());
            //builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(ServerVersion.AutoDetect(conn)));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<DatabaseHandlerService>();

            builder.Services.AddTransient<IEmailService, EmailService>();


            builder.Services.AddTransient<ProcessFinishedRepository>();
            builder.Services.AddTransient<WorkstationRulesRepository>();

            builder.Services.AddTransient<IWindowsWorkstationRepository, WindowsWorkstationRepository>();
            builder.Services.AddTransient<IProgramRepository, ProgramRepository>();
            builder.Services.AddTransient<IRuleRepository, RuleRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ProgramStartRepository>();

            builder.Services.AddTransient<CpuPerformanceRepository>();
            builder.Services.AddTransient<RamPerformanceRepository>();
            builder.Services.AddTransient<PerformanceSeparatorService>();
                 
            builder.Services.AddTransient<IPasswordService, PasswordService>();
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
