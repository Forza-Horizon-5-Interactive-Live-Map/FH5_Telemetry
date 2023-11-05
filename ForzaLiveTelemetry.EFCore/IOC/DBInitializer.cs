using ForzaLiveTelemetry.Domain.Entity;
using ForzaLiveTelemetry.Domain.Setting;
using Microsoft.AspNetCore.Identity;

namespace ForzaLiveTelemetry.EFCore.IOC;
public class DBInitializer
{
    public static async Task<bool> Initialize(UserContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        context.Database.EnsureCreated();


        if (context.Roles.Any() || context.Users.Any()) return false;


        //Adding roles
        string[] roles = Roles.GetAllRoles();

        foreach (string role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                IdentityResult resultAddRole = await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                if (!resultAddRole.Succeeded)
                    throw new ApplicationException("Adding role '" + role + "' failed with error(s): " + resultAddRole.Errors);
            }
        }

        //Adding Admin
        User admin = new()
        {
            UserName = "Dercraker",
            IPv4 = "192.168.1.254",
            CreatedAt = DateTime.Now,
        };

        string pwd = "NMdRx$HqyT8jX6";

        IdentityResult? resultAddUser = await userManager.CreateAsync(admin, pwd);
        if (!resultAddUser.Succeeded)
            throw new ApplicationException("Adding user '" + admin.UserName + "' failed with error(s): " + resultAddUser.Errors);

        IdentityResult resultAddRoleToUser = await userManager.AddToRoleAsync(admin, Roles.Admin);
        if (!resultAddRoleToUser.Succeeded)
            throw new ApplicationException("Adding user '" + admin.UserName + "' to role '" + Roles.Admin + "' failed with error(s): " + resultAddRoleToUser.Errors);


        await context.SaveChangesAsync();

        return true;
    }
}
