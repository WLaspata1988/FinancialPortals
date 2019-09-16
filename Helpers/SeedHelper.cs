using FinancialPortals.Enumerations;
using FinancialPortals.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FinancialPortals.Helpers
{
    public class SeedHelper
    {
        
        
        private readonly string DefaultAvatar = Setting.DefaultAvatar.ToString();
        private readonly string DefaultPassword = Setting.DefaultPassword.ToString();
        private readonly string SeededHouseName, SeededHeadOfHouseEmail; 
        private readonly string SeededHouseOwner = Setting.HeadOfHouse.ToString();

        private ApplicationDbContext Db { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        private string ReadKeyValue(Setting settings)
        {
            return WebConfigurationManager.AppSettings[settings.ToString()];
        }

        public SeedHelper()
        {
            Db = new ApplicationDbContext();
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Db));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Db));           
            SeededHouseName = ReadKeyValue(Setting.SeededHouseName);
            SeededHeadOfHouseEmail = ReadKeyValue(Setting.SeededHeadOfHouse);
        }
       
        #region roles
        public void SeedRole(Role role)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            var roleName = role.ToString();
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                roleManager.Create(new IdentityRole { Name = roleName });
            }         
        }

        public void SeedRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            foreach (Role role in Enum.GetValues(typeof(Role)).Cast<Role>().ToList())
            {
                SeedRole(role);
            }
            context.SaveChanges();
        }
        #endregion


        #region Users
        public void SeedUsers()
        {          
            foreach (var user in Enum.GetValues(typeof(Role)).Cast<Role>().ToList())
            {
                var userData = user.GetType().ToString();
                SeedUser(userData);
            }
        }
        public void SeedUser(string newData)
        {           
            var defaultAvatar = DefaultAvatar;
            var defaultPassword = DefaultPassword;
            var data = newData.Split('|');

            var email = data[0];
           
                if (!Db.Users.Any(u => u.Email == SeededHeadOfHouseEmail))
                {
                    UserManager.Create(new ApplicationUser
                    {                        
                        Email = SeededHeadOfHouseEmail,
                        UserName = SeededHeadOfHouseEmail,
                        FirstName = "Darth",
                        LastName = "Vader",
                        DisplayName = "Anakin",
                        Avatar = defaultAvatar
                    }, "abc&123!");

                    var user = UserManager.FindByEmail(SeededHeadOfHouseEmail);
                    user.HouseholdId = Db.Households.Where(h => h.Name == SeededHouseName).FirstOrDefault().Id;                   
                    Db.SaveChanges();                    
                }
            if (!Db.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                UserManager.Create(new ApplicationUser
                {
                    HouseholdId = null,
                    Email = "DemoAdmin@mailinator.com",
                    UserName = "DemoAdmin@mailinator.com",
                    FirstName = "William",
                    LastName = "Laspata",
                    DisplayName = "Will",
                    Avatar = DefaultAvatar
                }, "abc&123!");

                
                Db.SaveChanges();
            }
        }

        #endregion

        public void SeedHouse()
       {
            var owner = UserManager.FindByEmail(SeededHeadOfHouseEmail);

                Db.Households.AddOrUpdate(
                h => h.Name,
                    new Household
                    {   OwnerId = UserManager.FindByEmail(SeededHeadOfHouseEmail).Id,                                                
                        Name = SeededHouseName,
                        Greeting = WebConfigurationManager.AppSettings[Setting.SeededHouseGreeting.ToString()],
                        Established = DateTime.Now                        
                    });
            Db.SaveChanges();
        }

        public void AssignRoles()
        {                     
            foreach (var role in Enum.GetValues(typeof(Role)).Cast<Role>().ToList())
            {   
                var userId = UserManager.FindByEmail(SeededHeadOfHouseEmail).Id;
                UserManager.AddToRole(userId, role.ToString());
            }
            Db.SaveChanges();
        }



        public void SeedAccountTypes()
        {
            
            foreach (var type in Enum.GetValues(typeof(AccountTypeName)).Cast<AccountTypeName>().ToList())
            {
                Db.AccountTypes.AddOrUpdate(
                    t => t.Type,
                new AccountType
                {
                    Type = type.ToString(),
                    Description = "This" + type.ToString() + "of the type"
                });

            }
           Db.SaveChanges();
        }

        public void SeedTransactionTypes()
        {       
            foreach (var type in Enum.GetValues(typeof(TransactionTypeName)).Cast<TransactionTypeName>().ToList())
            {
                Db.TransactionTypes.AddOrUpdate(
                    t => t.Name,
                    new TransactionType
                    {
                        Name = type.ToString(),
                        Description = "This" + type.ToString() + "And that"
                    });
            }
            Db.SaveChanges();
        }

        public void SeedBudgetCategories()
        {            
            var seededHouseId = Db.Households.AsNoTracking().FirstOrDefault(h => h.Name == SeededHouseName).Id;
            var now = DateTime.Now;
            Db.BudgetCategories.AddOrUpdate(
                c => c.Name,
                new BudgetCategory { Name = "Household Utilities", HouseholdId = seededHouseId, Created = now, Target = 600.00, Actual = 400.00 },
                new BudgetCategory { Name = "Auto Maintenance", HouseholdId = seededHouseId, Created = now, Target = 500.00, Actual = 200.00 },
                new BudgetCategory { Name = "Living Expenses", HouseholdId = seededHouseId, Created = now, Target = 1000.00, Actual = 950.00 },
                new BudgetCategory { Name = "Discretionary", HouseholdId = seededHouseId, Created = now, Target = 200.00, Actual = 100.00 }
                );
            Db.SaveChanges();
        }

        public void SeedBudgetItems()
        {           
            var utilitiesBudgetId = Db.BudgetCategories.AsNoTracking().FirstOrDefault(b => b.Name == "Household Utilities").Id;
            var autoBudgetId = Db.BudgetCategories.AsNoTracking().FirstOrDefault(b => b.Name == "Auto Maintenance").Id;
            var livingBudgetId = Db.BudgetCategories.AsNoTracking().FirstOrDefault(b => b.Name == "Living Expenses").Id;
            var otherBudgetId = Db.BudgetCategories.AsNoTracking().FirstOrDefault(b => b.Name == "Discretionary").Id;
            var now = DateTime.Now;
            Db.BudgetItems.AddOrUpdate(
                new BudgetItem { Name = "Gas", BudgetCategoryId = utilitiesBudgetId, Created = now, Target = 150.00, Actual = 100.00 },
                new BudgetItem { Name = "Electric", BudgetCategoryId = utilitiesBudgetId, Created = now, Target = 150.00, Actual = 100.00 },
                new BudgetItem { Name = "Water/Sewage", BudgetCategoryId = utilitiesBudgetId, Created = now, Target = 150.00, Actual = 100.00 },
                new BudgetItem { Name = "Internet", BudgetCategoryId = utilitiesBudgetId, Created = now, Target = 200.00, Actual = 100.00 },
                new BudgetItem { Name = "Fuel", BudgetCategoryId = autoBudgetId, Created = now, Target = 200.00, Actual = 100.00 },
                new BudgetItem { Name = "Repairs", BudgetCategoryId = autoBudgetId, Created = now, Target = 200.00, Actual = 100.00 },
                new BudgetItem { Name = "Clothing", BudgetCategoryId = livingBudgetId, Created = now, Target = 200.00, Actual = 100.00 },
                new BudgetItem { Name = "Groceries", BudgetCategoryId = livingBudgetId, Created = now, Target = 200.00, Actual = 100.00 },
                new BudgetItem { Name = "Rent/Mortgage", BudgetCategoryId = livingBudgetId, Created = now, Target = 800.00, Actual = 750.00 },
                new BudgetItem { Name = "Entertainment", BudgetCategoryId = otherBudgetId, Created = now, Target = 200.00, Actual = 100.00 }
                );
            Db.SaveChanges();
        }

        public void SeedTransactions()
        {            
            var accountTypes = Db.AccountTypes.AsNoTracking().ToList();
            var transactionTypes = Db.TransactionTypes.AsNoTracking().ToList();
            var ownerId = UserManager.FindByEmail(SeededHeadOfHouseEmail).Id;
            var savingsAccountId = accountTypes.FirstOrDefault(a => a.Type == AccountTypeName.Savings.ToString()).Id;
            var bankAccountId = accountTypes.FirstOrDefault(a => a.Type == AccountTypeName.Checking.ToString()).Id;
            var transactionTypeId = transactionTypes.FirstOrDefault(t => t.Name ==TransactionTypeName.Withdrawal.ToString()).Id;
            var depTransactionTypeId = transactionTypes.FirstOrDefault(t => t.Name == TransactionTypeName.Deposit.ToString()).Id;
            var gasBudgetItemId = Db.BudgetItems.AsNoTracking().FirstOrDefault(t => t.Name == "Gas").Id;
            var entertainmentBudgetItemId = Db.BudgetItems.AsNoTracking().FirstOrDefault(t => t.Name == "Entertainment").Id;
            var now = DateTime.Now;

            //Two transactions coming from my checking account

            //Transaction 1
            Db.Transactions.AddOrUpdate(
                t => t.TransactionType,
                new Transaction
                {
                    OwnerId = ownerId,
                    BankAccountId = bankAccountId,
                    BudgetItemId = gasBudgetItemId,
                    TransactionTypeId = transactionTypeId,
                    Amount = 100.00,
                    Memo = "What a waste of gas.",
                    Created = now                    
                },

            //Transaction 2
            
                new Transaction
                {
                    OwnerId = ownerId,
                    BankAccountId = bankAccountId,
                    BudgetItemId = entertainmentBudgetItemId,
                    TransactionTypeId = transactionTypeId,
                    Amount = 100.00,
                    Memo = "I love gaming!",
                    Created = now                   
                },

            //Savings account deposit of $100.00

                new Transaction
                {
                    OwnerId = ownerId,
                    BankAccountId = savingsAccountId,
                    TransactionTypeId = depTransactionTypeId,
                    Amount = 100.00,
                    Memo = "I guess I kinda like money.",
                    Created = now
                });
            Db.SaveChanges();
        }

        public void SeedBankAccount()
        {
            var seededHouseId = Db.Households.Where(h => h.Name == SeededHouseName).FirstOrDefault().Id;
            var accountTypes = Db.AccountTypes.AsNoTracking().AsQueryable();
            var ownerId = UserManager.FindByEmail(SeededHeadOfHouseEmail).Id;
            Db.BankAccounts.AddOrUpdate(
                t => t.Name,
                new BankAccount
                {
                    HouseholdId = seededHouseId,
                    AccountTypeId = accountTypes.FirstOrDefault(a => a.Type == AccountTypeName.Checking.ToString()).Id,
                    OwnerId = ownerId,
                    StartingBalance = 10000.00,
                    CurrentBalance = 7000.00,
                    LowBalance = 5000.00,
                    Name = "Allegacy Checking",
                    Description = "Seeded Federal Credit Union Checking",
                    Address1 = "Allegacy Way",
                    Address2 = "None",
                    City = "Clemmons",
                    State = State.NC,
                    Zip = "27012",
                    Phone = "336-xxx-xxxx",
                    Created = DateTime.Now,         
                    ReconciledBalance = 0
                },            
               new BankAccount
               {
                   HouseholdId = seededHouseId,
                   AccountTypeId = accountTypes.FirstOrDefault(a => a.Type == AccountTypeName.Savings.ToString()).Id,
                   OwnerId = ownerId,
                   StartingBalance = 5000.00,
                   CurrentBalance = 5000.00,
                   LowBalance = 2000.00,
                   Name = "Allegacy Savings",
                   Description = "Seeded Federal Credit Union Savings",
                   Address1 = "Allegacy Way",
                   Address2 = "None",
                   City = "Clemmons",
                   State = State.NC,
                   Zip = "27012",
                   Phone = "336-xxx-xxxx",
                   Created = DateTime.Now,
                   ReconciledBalance = 0
               });
            Db.SaveChanges();
        }

        public void SeedNotification()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var ownerId = userManager.Find(SeededHouseOwner, DefaultPassword).Id;
            var seededHouseId = context.Households.AsNoTracking().FirstOrDefault(h => h.Name == SeededHouseName).Id;

            context.Notifications.AddOrUpdate(
                n => n.Subject,
                new Notification
                {
                    Created = DateTime.Now,
                    OwnerId = ownerId,
                    HouseholdId = seededHouseId,
                    Body = "Any string you'd like.",
                    Subject = "HEY!"
                });
        }
              
    
    }
}