using SchoolManagement.Migrator;

using SchoolManagement.Domain.Extensions;
using SchoolManagement.Infrastructure.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddDomainServices()
    .AddInfrastructureServices(builder.Configuration);

builder.Services.RegisterDateSeeder();

builder.Services.AddHostedService<Migrator>();

var host = builder.Build();
host.Run();