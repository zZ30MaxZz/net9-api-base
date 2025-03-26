using Dokypets.Common.Enums;
using Dokypets.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Dokypets.Infrastructure.Identity.Seeds
{
    public class DefaultUsersSeed
    {
        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            #region defaultUser1
            var defaultUser1 = new ApplicationUser
            {
                UserName = "moka",
                Email = "moka@mail.com",
                FirstName = "Moka",
                LastName = "Aguilar Arroyo",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser1.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser1.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser1, "Moka.123");
                    await userManager.AddToRoleAsync(defaultUser1, Roles.SuperAdmin.ToString());
                }
            }
            #endregion


            #region defaultUser2

            var defaultUser2 = new ApplicationUser
            {
                UserName = "Mila",
                Email = "mila@mail.com",
                FirstName = "Mila",
                LastName = "Aguilar Arroyo",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser2, "Mila.123");
                    await userManager.AddToRoleAsync(defaultUser2, Roles.User.ToString());
                }
            }
            #endregion

        }
    }
}