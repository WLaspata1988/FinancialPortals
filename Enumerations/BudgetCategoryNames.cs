using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortals.Enumerations
{
    public enum BudgetCategoryNames
    {
        [Display(Name = "Household Utilities")]
        HouseholdUtilities,
        [Display(Name = "Auto Maintenance")]
        AutomotiveMaintenance,
        [Display(Name = "Living Expenses")]
        LivingExpenses,




    }
}