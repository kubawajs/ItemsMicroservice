using Microsoft.AspNetCore.Identity;

namespace ItemsMicroservice.Infrastructure.Database;

public static class StaticDbInitializer
{
    public static async Task CreateUsersAsync(UserManager<IdentityUser> userManager)
    {
        // Add "Administracja Items" user
        IdentityUser user = new()
        {
            UserName = "user"
        };

        await userManager.CreateAsync(user, "User123$");

        // Add "Zarządzanie Items - administracja" user
        user = new()
        {
            UserName = "admin"
        };

        var result = await userManager.CreateAsync(user, "Admin123$");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, Constants.Users.Roles.Admin);
        }
    }
}
