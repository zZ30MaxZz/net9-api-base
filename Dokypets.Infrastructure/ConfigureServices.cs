using Dokypets.Application.Interface.Persistence;
using Dokypets.Infrastructure.Contexts;
using Dokypets.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Dokypets.Application.Interface.Persistence.Identity;
using Dokypets.Infrastructure.Repositories.Identity;

namespace Dokypets.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            #region
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            #endregion


            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
