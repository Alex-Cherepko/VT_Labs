using CherepkoLib.Data;
using CherepkoLib.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cherepko.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                // создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }
            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "321321");
                // создать пользователя admin@mail.ru
                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "321321");
                // назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }

            //проверка наличия групп объектов
            if (!context.RodGroups.Any())
            {
                context.RodGroups.AddRange(
                new List<RodGroup>
                {
                    new RodGroup {GroupName="Picker"},
                    new RodGroup {GroupName="Medium"},
                    new RodGroup {GroupName="Heavy"},
                });
                await context.SaveChangesAsync();
            }

            // проверка наличия объектов
            if (!context.Rods.Any())
            {
                context.Rods.AddRange(
                new List<Rod>
                {
                    new Rod {RodName="ZEMEX Hi-Pro Super Feeder",
                    Description="Длина 10ft тест 50g",
                    Price =320.00f, RodGroupId=1, Image="hi_pro_new_image_10.jpg" },
                    new Rod {RodName="ZEMEX Razer F-1 Carp Mini Feeder",
                    Description="Длина 11ft тест 60g",
                    Price =560.00f, RodGroupId=1, Image="razer_new_image_11.jpg" },
                    new Rod {RodName="ZEMEX Iron Feeder",
                    Description="Длина 10ft тест 40g",
                    Price =180.00f, RodGroupId=1, Image="iron_feeder_image_10.jpg" },

                    new Rod {RodName="ZEMEX Hi-Pro Super Feeder",
                    Description="Длина 12ft тест 80g",
                    Price =350.00f, RodGroupId=2, Image="hi_pro_new_image_10.jpg" },
                    new Rod {RodName="ZEMEX Razer F-1 Carp Mini Feeder",
                    Description="Длина 12ft тест 80g",
                    Price =570.00f, RodGroupId=2, Image="razer_new_image_11.jpg" },
                    new Rod {RodName="ZEMEX Iron Feeder",
                    Description="Длина 12ft тест 90g",
                    Price =200.00f, RodGroupId=2, Image="iron_feeder_image_10.jpg" },

                    new Rod {RodName="ZEMEX Hi-Pro Super Feeder",
                    Description="Длина 13ft тест 140g",
                    Price =400.00f, RodGroupId=3, Image="hi_pro_new_image_10.jpg" },
                    new Rod {RodName="ZEMEX Razer Method Feeder",
                    Description="Длина 14ft тест 140g",
                    Price =700.00f, RodGroupId=3, Image="razer_new_image_11.jpg" },
                    new Rod {RodName="ZEMEX Iron Feeder",
                    Description="Длина 13ft тест 140g",
                    Price =220.00f, RodGroupId=3, Image="iron_feeder_image_10.jpg" }

                });
                await context.SaveChangesAsync();
            }

        }
    }
}
