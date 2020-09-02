using FinancialPortal.Enums;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinancialPortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Display(Name = "Bank Account")]
        public int AccountId { get; set; }
        public virtual BankAccount Account { get; set; }
        public int? BudgetItemId { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }
        //this will indicate what the money was spent on
        public string Memo { get; set; }
        [Display(Name = "Delete Transaction")]
        public bool IsDeleted { get; set; }
        public Transaction()
        {
            Created = DateTime.Now;
            OwnerId = HttpContext.Current.User.Identity.GetUserId();
        }
    }
}