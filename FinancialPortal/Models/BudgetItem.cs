using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
        public DateTime Created { get; set; }
        [Display(Name = "Name")]
        public string ItemName { get; set; }
        [Display(Name = "Target Amount")]
        public decimal TargetAmount { get; set; }
        [Display(Name = "Current Amount")]
        public decimal CurrentAmount { get; set; }
        [Display(Name = "Delete Budget Item")]
        public bool IsDeleted { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public BudgetItem()
        {
            Transactions = new HashSet<Transaction>();
            Created = DateTime.Now;
            CurrentAmount = 0;
        }
    }
}