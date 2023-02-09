using ItemsMicroservice.Core.Domain;
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

    public static async Task CreateColorsAsync(ItemsMicroserviceDbContext context)
    {
        var colors = new[]
        {
            new Color { Id = 1, Name = "red" },
            new Color { Id = 2, Name = "green" },
            new Color { Id = 3, Name = "blue" },
            new Color { Id = 4, Name = "yellow" },
            new Color { Id = 5, Name = "white" },
            new Color { Id = 6, Name = "black" }
        };
        await context.AddRangeAsync(colors);
        await context.SaveChangesAsync();
    }

    public static async Task CreateItemsAsync(ItemsMicroserviceDbContext context)
    {
        var items = new List<Item>();
        var colors = new[] { "red", "green", "blue", "yellow", "white", "black" };
        var random = new Random();
        for(var index = 0; index < 200000; index++)
        {
            items.Add(new Item
            {
                Code = $"code-{index}",
                Name = $"NAME-{index}",
                Notes = "Lorem ipsum dolor sit amet.",
                Color = colors[random.Next(colors.Length)]
            });
        }

        await context.AddRangeAsync(items);
        await context.SaveChangesAsync();
    }
}
