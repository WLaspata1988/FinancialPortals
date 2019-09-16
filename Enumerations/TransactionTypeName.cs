using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortals.Enumerations
{
    public enum TransactionTypeName
    {
        Deposit,
        Withdrawal,
        [Display(Name = "Adjustment Up")]
        AdjustmentUp,
        [Display(Name = "Adjustment Down")]
        AdjustmentDown,
        Reconciliation
    }
}