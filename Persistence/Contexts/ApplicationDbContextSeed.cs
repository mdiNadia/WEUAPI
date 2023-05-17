using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Contexts
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
        {
            //Seed Roles
            if (!applicationDbContext.ApplicationRoles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.Administrator.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
            }
            //Seed Default Language
            if (!applicationDbContext.AppSettings.Any())
            {
                var model = new AppSetting()
                {
                    AppFee = 30,// تعرفه ما که از مشتری میگیریم. مثلا 30 درصد
                    Value = 1,// مثلا ارزش هر ویو 1 از یک دلار است
                    MinValuePerVisit = 1,//حداقل ویو بابت هر بازدید که به یک یوزر میدیم
                    MinView = 12,
                    MinBoostAmount = 120000,
                    CreationDate = DateTime.Now,
                };
                applicationDbContext.AppSettings.Add(model);
                await applicationDbContext.SaveChangesAsync();
            }
            if (!applicationDbContext.Languages.Any())
            {
                var model = new Language()
                {
                    ShortName = "fa",
                    IsDefault = true,
                    CreationDate = DateTime.Now,
                    Name = "فارسی",
                    Direction = 0,
                };
                applicationDbContext.Languages.Add(model);
                await applicationDbContext.SaveChangesAsync();
            }
            //Seed Default Country
            if (!applicationDbContext.Countries.Any())
            {
                var model = new Country()
                {
                    Name = "ایران",
                    Iso = "IR",
                    Iso3 = "IRN",
                    NumCode = 364,
                    PhoneCode = 98,

                };
                applicationDbContext.Countries.Add(model);
                await applicationDbContext.SaveChangesAsync();

            }
            //Seed Default Currency
            if (!applicationDbContext.Currencies.Any())
            {
                var model = new Currency()
                {
                    IsDefault = true,
                    CurrencyName = "ریال",
                    IsActive = true,

                };
                applicationDbContext.Currencies.Add(model);
                await applicationDbContext.SaveChangesAsync();
            }
            //Seed Default User
            var defaultUser = new ApplicationUser { UserName = Authorization.default_username, Email = Authorization.default_email, EmailConfirmed = true, PhoneNumberConfirmed = true, FirstName = Authorization.default_firstname, LastName = Authorization.default_lastname, PhoneNumber = Authorization.default_phonenumber };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.default_role.ToString());
                var model = new Profile();
                model.UserId = defaultUser.Id;
                model.Username = defaultUser.UserName;
                applicationDbContext.Profiles.Add(model);
                await applicationDbContext.SaveChangesAsync();

            }
        }
    }
}
