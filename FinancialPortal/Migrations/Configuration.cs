namespace FinancialPortal.Migrations
{
    using FinancialPortal.Enums;
    using FinancialPortal.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<FinancialPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FinancialPortal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            #region Role Creation
            var roleManager = new RoleManager<IdentityRole>(
                              new RoleStore<IdentityRole>(context));
            //Represents a role for testing and debugging
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole() { Name = "Admin" });
            }
            //Represents the Head of Household role
            if (!context.Roles.Any(r => r.Name == "Head"))
            {
                roleManager.Create(new IdentityRole() { Name = "Head" });
            }
            //Represents a user who is part of a household, but not in the Head of Household role (registers with an invitation code)
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole() { Name = "Member" });
            }
            //Represents a new user who has not joined or created a Household (registers without using an invitation code, or leaves a household)
            if (!context.Roles.Any(r => r.Name == "New User"))
            {
                roleManager.Create(new IdentityRole() { Name = "New User" });
            }
            #endregion

            #region User Creation
            var adminEmail = WebConfigurationManager.AppSettings[ApplicationSettings.AdminEmail.ToString()];
            var adminPassword = WebConfigurationManager.AppSettings["AdminPassword"];
            var memberEmail = WebConfigurationManager.AppSettings[ApplicationSettings.MemberEmail.ToString()];
            var memberPassword = WebConfigurationManager.AppSettings["MemberPassword"];
            var userManager = new UserManager<ApplicationUser>
                             (new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == adminEmail))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "Demo",
                    LastName = "Admin",
                }, adminPassword);
                var userId = userManager.FindByEmail(adminEmail).Id;
                userManager.AddToRole(userId, "Admin");
            }
            if (!context.Users.Any(u => u.Email == memberEmail))
            {
                userManager.Create(new ApplicationUser()
                {
                    Email = memberEmail,
                    UserName = memberEmail,
                    FirstName = "Demo",
                    LastName = "Member",
                }, memberPassword);
                var userId = userManager.FindByEmail(memberEmail).Id;
                userManager.AddToRole(userId, "Member");
            }
            #endregion

            #region Seed Household
            Household newHouse = null;
            if (!context.Households.Any())
            {
                newHouse = new Household
                {
                    Created = DateTime.Now,
                    Greeting = "Hello Demo House",
                    IsDeleted = false,
                    HouseholdName = "Seeded House",
                };
                context.Households.Add(newHouse);
            }
            context.SaveChanges();
            #endregion

            #region Seed Bank Account
            var ownerId = context.Users.FirstOrDefault(u => u.Email == adminEmail).Id;
            if (!context.BankAccounts.Any())
            {
                context.BankAccounts.Add(new BankAccount
                {
                    AccountName = "Wells Fargo Checking",
                    AccountType = AccountType.Checking,
                    Created = DateTime.Now,
                    CurrentBalance = 5000,
                    HouseholdId = newHouse.Id,
                    IsDeleted = false,
                    OwnerId = ownerId,
                    StartingBalance = 5000,
                    WarningBalance = 500,
                });
            context.SaveChanges();
            }
            #endregion

            #region Seed Budget
            Budget budget = null;
            if (!context.Budgets.Any())
            {
                budget = new Budget(true)
                {
                    BudgetName = "Utilities",
                    Created = DateTime.Now,
                    HouseholdId = newHouse.Id,
                    OwnerId = ownerId,
                    CurrentAmount = 0,
                };
                context.Budgets.Add(budget);
                context.SaveChanges();
            };
            
            #endregion

            #region Seed Item
            if (!context.BudgetItems.Any())
            {
                context.BudgetItems.Add(new BudgetItem()
                {
                   CurrentAmount = 0,
                   TargetAmount = 250,
                   BudgetId = budget.Id,
                   Created = DateTime.Now,
                   ItemName = "Gas Bill",
                   IsDeleted = false,
                });
                context.SaveChanges();
            }
            #endregion

            #region Seed Transaction(s)
            #endregion
        }
    }
}
