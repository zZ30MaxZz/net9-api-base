using Dokypets.Domain.Entities;
using Dokypets.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Dokypets.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext([NotNull] DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserToken>()
                .ToTable("AspNetUserTokens")
                .Property(t => t.ExpiryDate)
                .IsRequired();

            builder.Entity<ApplicationUserToken>()
                .Property(t => t.IsRevoked)
                .HasDefaultValue(false);
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
