using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortals.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int AccountTypeId { get; set; }
        public string OwnerId { get; set; }
        public double StartingBalance { get; set; }
        public double CurrentBalance { get; set; }
        public double LowBalance { get; set; }        
        [MaxLength(50, ErrorMessage = "Bank Name cannot be greater than 50 characters")]
        [MinLength(1, ErrorMessage = "Bank Name is required")]
        public string Name { get; set; }   
        public string Description { get; set; }
        
        [MaxLength(100, ErrorMessage = "Address cannot be greater than 100 characters")]
        [MinLength(1, ErrorMessage = "Address is required")]
        public string Address1 { get; set; }        
        [MaxLength(100, ErrorMessage = "Address cannot be greater than 100 characters")]
        [MinLength(1, ErrorMessage = "Address is required")]
        public string Address2 { get; set; }        
        [MaxLength(50, ErrorMessage = "City cannot be greater than 50 characters")]
        [MinLength(1, ErrorMessage = "City is required")]
        public string City { get; set; }
        public State State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }


        public DateTime Created { get; set; }
        public int ReconciledBalance { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual Household Household { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        
        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }

    }
}