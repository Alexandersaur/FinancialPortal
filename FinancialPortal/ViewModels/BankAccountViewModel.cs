using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class BankAccountViewModel
    {
        public BankAccount BankAccount { get; set; }
        public decimal MonthlySpending { get; set; }
        public int MonthlyTransactions { get; set; }
        public decimal MonthlyDeposits { get; set; }


    }
}