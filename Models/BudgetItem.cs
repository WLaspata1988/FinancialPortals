using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinancialPortals.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public int BudgetCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Target { get; set; }
        public double Actual { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual BudgetCategory BudgetCategory { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}