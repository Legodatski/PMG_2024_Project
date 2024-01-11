using Microsoft.AspNetCore.Identity;
using TaxSystem.Data;

namespace TaxSystem.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedRoles(
            this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole adminRole = new IdentityRole("Admin");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    IdentityRole userRole = new IdentityRole("User");
                    await roleManager.CreateAsync(userRole);
                }

                if (!await roleManager.RoleExistsAsync("Worker"))
                {
                    IdentityRole userRole = new IdentityRole("Worker");
                    await roleManager.CreateAsync(userRole);
                }

                ApplicationUser admin = await userManager.FindByEmailAsync("admin1@gmail.com");


                if (admin == null)
                {
                    await userManager.CreateAsync(new ApplicationUser()
                    {
                        Id = "35bbae7d-b2a0-472a-8137-e8df5f4ac614",
                        FirstName = "Admin",
                        LastName = "Adminov",
                        Email = "admin1@gmail.com",
                        UserName = "Admin"
                    }, "admin123");

                    admin = await userManager.FindByEmailAsync("admin1@gmail.com");
                }

                await userManager.AddToRoleAsync(admin, "Admin");
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
