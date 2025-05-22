using Avalanche.Core.Application.Enums;
using Avalanche.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Avalanche.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "superAdminUser";
            defaultUser.Email = "superadminuser@email.com";
            defaultUser.FirstName = "David";
            defaultUser.LastName = "de la Rosa";
            defaultUser.Address = "Brisas del Este";
            defaultUser.UrlImage = "no hay por ahora";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            await userManager.CreateAsync(defaultUser, "1505Pa@@word");
            await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());
            await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
            await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());

        }
    }
}
