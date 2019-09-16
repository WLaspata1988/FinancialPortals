using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortals.Models
{
    public class AccountType
    {
        public int Id { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }

        public AccountType()
        {
            BankAccounts = new HashSet<BankAccount>();
        }

    }
}