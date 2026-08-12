using AutoParts.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace AutoParts.Data.Seed
{
    public static class IdentityInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            const string userName = "admin";
            const string email = "admin@autoparts.local";
            const string password = "Admin@123";

            var user = await userManager.FindByNameAsync(userName);

            if (user != null) 
                return;

            user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Erro ao criar usuário administrador: {errors}");
            }
        }
    }
}
