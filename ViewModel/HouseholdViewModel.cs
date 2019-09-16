using FinancialPortals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortals.ViewModel
{
    public class HouseholdViewModel
    {
        public Household Household = new Household();       
        public BankAccount BankAccount = new BankAccount();        
        public BudgetCategory BudgetCategory = new BudgetCategory();
        public BudgetItem BudgetCategoryItem = new BudgetItem();
        public AccountType AccountType = new AccountType();
    }
}