using System.Text;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.DataSeeders;
using SchoolManagement.Infrastructure;

namespace SchoolManagement.Migrator;

public class Migrator : IHostedService
{
    private readonly ILogger<Migrator> _logger;
    private SchoolManagementDbContext _dbContext;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IServiceScopeFactory _scopeFactory;
    private DataSeeder _dataSeeder;

    public Migrator(ILogger<Migrator> logger, IHostApplicationLifetime hostApplicationLifetime,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _hostApplicationLifetime = hostApplicationLifetime;
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started database migrations...");
        using var scope = _scopeFactory.CreateScope();

        _dbContext = scope.ServiceProvider.GetRequiredService<SchoolManagementDbContext>();
        StringBuilder pendingMigrationsMessage = new StringBuilder();
        pendingMigrationsMessage.AppendLine("Pending Migrations :");
        var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
        foreach (var pendingMigration in pendingMigrations)
        {
            pendingMigrationsMessage.AppendLine(pendingMigration);
        }

        _logger.LogInformation(pendingMigrationsMessage.ToString());

        await _dbContext.Database.MigrateAsync(cancellationToken);

        _dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await _dataSeeder.SeedAsync();
        _hostApplicationLifetime.StopApplication();
        _logger.LogInformation($"Successfully completed host database migrations.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Migrator ShutDown");
        return Task.CompletedTask;
    }
}