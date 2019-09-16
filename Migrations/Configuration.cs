namespace FinancialPortals.Migrations
{
    using FinancialPortals.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FinancialPortals.Enumerations;
    using FinancialPortals.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        RoleManager<IdentityRole> RoleManager { get; set; }
            private SeedHelper seedHelper = new SeedHelper();


        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            #region Seed Roles

            seedHelper.SeedUsers();
            seedHelper.SeedRoles();
            seedHelper.SeedHouse();
            seedHelper.AssignRoles();


            seedHelper.SeedAccountTypes();
            seedHelper.SeedBankAccount();

            seedHelper.SeedTransactionTypes();
            seedHelper.SeedBudgetCategories();
            seedHelper.SeedBudgetItems();
            seedHelper.SeedTransactions();


            #endregion




        }
        private string StringRole(Role role)
                {
            return role.ToString();
                }
    }
}
