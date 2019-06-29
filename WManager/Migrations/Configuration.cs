namespace WManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WManager.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WManager.Controllers;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<WManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WManager.Models.ApplicationDbContext context)
        {
            IdentityRole MenadzerRole = context.Roles.FirstOrDefault(x => x.Name == "Menadzer") ?? new IdentityRole("Menadzer");
            IdentityRole RadnikRole = context.Roles.FirstOrDefault(x => x.Name == "Radnik") ?? new IdentityRole("Radnik");
            
            context.Roles.AddOrUpdate(MenadzerRole);
            context.Roles.AddOrUpdate(RadnikRole);
            context.SaveChanges();
            ApplicationUser menadzer = context.Users.FirstOrDefault(x => x.UserName == "menadzer@gmail.com");
            if (menadzer == null)
            {

                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = new ApplicationUser() { Email = "menadzer@gmail.com", UserName = "menadzer@gmail.com", FirstName = "Pera", LastName = "Peric", PhoneNumber = "064/111-2121" };
                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "Menadzer");

            }

        }
    }
}
