using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Security;
using SchoolManagement.Infrastructure.Security;

namespace SchoolManagement.Presentation.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtSettingsKey));
        
        var jwtSettings = configuration.GetSection(JwtSettings.JwtSettingsKey).Get<JwtSettings>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer ?? throw new ArgumentException("Issuer is required"),
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

        
        services.AddAuthorization();

        services.AddAuthorization();
        services.AddScoped<ITokenGenerator, TokenGenerator>();

       
        services.AddControllers();
        services.AddSwaggerGen(
            options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.OperationFilter<SwaggerAuthorizationFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolManagement APIs", Version = "v1" });
            });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<ICurrentUser, CurrentUserService>();
        
        return services;
    }
}