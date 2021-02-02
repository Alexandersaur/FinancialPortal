using FinancialPortal.Models;
using FinancialPortal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialPortal.ViewModels
{
    public class BankAccountViewModel
    {
        public BankAccount BankAccount { get; set; }
        public decimal MonthlySpending { get; set; }
        public int MonthlyTransactions { get; set; }
        public decimal MonthlyDeposits { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalMonthlySpending { get; set; }
        public decimal TotalMonthlyDeposits { get; set; }
        public Transaction Transaction { get; set; } = new Transaction();
        public SelectList BudgetItemId { get; set; }
        public List<decimal> ApexSeries { get; set; } = new List<decimal>();
        public List<string> ApexLabels { get; set; } = new List<string>();
        public List<string> ApexColors { get; set; } = new List<string>();

        //public string Memo { get; set; }
        //public TransactionType TransactionType { get; set; }
        //public DateTime Created { get; set; }
        //public decimal Amount { get; set; }

    }
}