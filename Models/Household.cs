using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortals.Models
{
    public class Household
    {
        public int Id { get; set; }        
        public string OwnerId { get; set; }

        public string Name { get; set; }
        public string Greeting { get; set; }
        public DateTime Established { get; set; }
        public bool IsConfigured { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<BudgetCategory> BudgetCategories { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<Notification> Notifications  { get; set; }

        public Household()
        {
            BankAccounts = new HashSet<BankAccount>();
            BudgetCategories = new HashSet<BudgetCategory>();
            Invitations = new HashSet<Invitation>();
            Notifications = new HashSet<Notification>();
            Members = new HashSet<ApplicationUser>();
        }

    }
}