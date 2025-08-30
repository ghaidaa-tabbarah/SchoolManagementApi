using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.DataSeeders;
using SchoolManagement.Domain.Security;
using SchoolManagement.Domain.UnitOfWorks;
using SchoolManagement.Infrastructure.Base;
using SchoolManagement.Infrastructure.Security;
using SchoolManagement.Infrastructure.UnitOfWorks;


namespace SchoolManagement.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddTransient<IAsyncQueryableExecutor, AsyncQueryableExecutor>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<SchoolManagementDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("MsSql")
                , b => b.MigrationsAssembly("SchoolManagement.Infrastructure")
            );
        });
        return services;
    }

    public static IServiceCollection RegisterDateSeeder(this IServiceCollection services)
    {
        services.AddTransient<DataSeeder>();
        return services;
    }
}