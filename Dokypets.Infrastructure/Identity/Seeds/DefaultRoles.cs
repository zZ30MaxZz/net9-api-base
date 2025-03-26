using Dokypets.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Dokypets.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.SuperAdmin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));

            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

            if (!await roleManager.RoleExistsAsync(Roles.Moderator.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));

            if (!await roleManager.RoleExistsAsync(Roles.User.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
    }
}