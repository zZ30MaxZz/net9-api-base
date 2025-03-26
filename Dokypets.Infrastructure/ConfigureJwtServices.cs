using Dokypets.Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Dokypets.Domain.Entities.Identity;
using Dokypets.Domain.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Dokypets.Infrastructure
{
    public class JwtErrorResponse
    {
        public int status { get; internal set; }
        public string? message { get; set; }
    }

    public static class ConfigureJwtServices
    {
        public static IServiceCollection AddJwtInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWT"));

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var identitySettings = configuration.GetSection("IdentitySettings").Get<IdentitySettings>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = identitySettings.Password.RequireDigit;
                options.Password.RequireLowercase = identitySettings.Password.RequireLowercase;
                options.Password.RequireUppercase = identitySettings.Password.RequireUppercase;
                options.Password.RequireNonAlphanumeric = identitySettings.Password.RequireNonAlphanumeric;
                options.Password.RequiredLength = identitySettings.Password.RequiredLength;
                options.Password.RequiredUniqueChars = identitySettings.Password.RequiredUniqueChars;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = identitySettings.Lockout.MaxFailedAccessAttempts;
                options.Lockout.DefaultLockoutTimeSpan = identitySettings.Lockout.DefaultLockoutTimeSpan;
                options.Lockout.AllowedForNewUsers = identitySettings.Lockout.AllowedForNewUsers;
                options.SignIn.RequireConfirmedEmail = identitySettings.SignIn.RequireConfirmedEmail;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(b =>
                {
                    b.RequireHttpsMetadata = false;
                    b.SaveToken = false;
                    b.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };

                    b.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            var isTokenExpired = context.AuthenticateFailure?.GetType() == typeof(SecurityTokenExpiredException);

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new JwtErrorResponse
                            {
                                status = 401,
                                message = isTokenExpired ? "Token has expired" : "You are not authorized"
                            });
                            await context.Response.WriteAsync(result);
                        },
                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new JwtErrorResponse
                            {
                                status = 403,
                                message = "You don't have permission to access this resource",
                            });
                            await context.Response.WriteAsync(result);
                        }
                    };
                });

            return services;
        }
    }
}
