using Microsoft.AspNetCore.Identity;
using Real_Estate_Rental_Practic.Areas.Identity.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedRolesAsync(roleManager);
        await SeedUsersAsync(userManager, roleManager);
    }

    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "AgencyManager" };

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string adminEmail1 = "Admin1@gmail.com";
        string adminPassword1 = "Admin1@gmail.com";
        string adminEmail2 = "Admin2@gmail.com";
        string adminPassword2 = "Admin2@gmail.com";

        if (await userManager.FindByEmailAsync(adminEmail1) == null)
        {
            var admin1 = new ApplicationUser { UserName = adminEmail1, Email = adminEmail1 };
            await userManager.CreateAsync(admin1, adminPassword1);
            await userManager.AddToRoleAsync(admin1, "Admin");
        }

        if (await userManager.FindByEmailAsync(adminEmail2) == null)
        {
            var admin2 = new ApplicationUser { UserName = adminEmail2, Email = adminEmail2 };
            await userManager.CreateAsync(admin2, adminPassword2);
            await userManager.AddToRoleAsync(admin2, "Admin");
        }

        string agencyManagerEmail1 = "AgencyManager1@gmail.com";
        string agencyManagerPassword1 = "AgencyManager1@gmail.com";
        string agencyManagerEmail2 = "AgencyManager2@gmail.com";
        string agencyManagerPassword2 = "AgencyManager2@gmail.com";

        if (await userManager.FindByEmailAsync(agencyManagerEmail1) == null)
        {
            var agencyManager1 = new ApplicationUser { UserName = agencyManagerEmail1, Email = agencyManagerEmail1 };
            await userManager.CreateAsync(agencyManager1, agencyManagerPassword1);
            await userManager.AddToRoleAsync(agencyManager1, "AgencyManager");
        }

        if (await userManager.FindByEmailAsync(agencyManagerEmail2) == null)
        {
            var agencyManager2 = new ApplicationUser { UserName = agencyManagerEmail2, Email = agencyManagerEmail2 };
            await userManager.CreateAsync(agencyManager2, agencyManagerPassword2);
            await userManager.AddToRoleAsync(agencyManager2, "AgencyManager");
        }
    }
}