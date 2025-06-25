using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence
{
    public class DataSeeding(
           UserManager<IdentityUser> _userManager,
           RoleManager<IdentityRole> _roleManager,
           StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
        public async Task IdentityDataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _identityDbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                    await _identityDbContext.Database.MigrateAsync();

                }
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new IdentityUser()
                    {
                        Email = "Mohamed@gmail.com",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };
                    var User02 = new IdentityUser()
                    {
                        Email = "Salma@gmail.com",
                        PhoneNumber = "0123456789",
                        UserName = "salmaMohamed"
                    };
                    await _userManager.CreateAsync(User01, "P@ss0rd");
                    await _userManager.CreateAsync(User02, "P@ss0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                }
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 

            }
           
        }
    }
}
