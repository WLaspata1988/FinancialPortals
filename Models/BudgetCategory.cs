using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinancialPortals.Models
{    
    public class BudgetCategory
    {     
        public int Id { get; set; }
        public int HouseholdId { get; set; }
       
        [MaxLength(50, ErrorMessage = "Name cannot be greater than 50 characters")]
        [MinLength(1, ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public double Target { get; set; }
        public double Actual { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Household Household { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public BudgetCategory()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }


    }
}