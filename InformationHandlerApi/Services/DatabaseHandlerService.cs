
using InformationHandlerApi.Database;

namespace InformationHandlerApi.Services
{
    public class DatabaseHandlerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseHandlerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            _ = await context.Database.EnsureCreatedAsync(stoppingToken);
        }
    }
}