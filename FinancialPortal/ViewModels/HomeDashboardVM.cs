using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialPortal.Models;

namespace FinancialPortal.ViewModels
{
    public class HomeDashboardVM
    {
        public ICollection<Transaction> RecentTransactions { get; set; }

        public ICollection<BankAccount> HouseAccounts { get; set; }

        public ICollection<BankAccount> UserAccounts { get; set; }

        public Household UserHouse { get; set; }

        public HomeDashboardVM()
        {
            RecentTransactions = new HashSet<Transaction>();
            HouseAccounts = new HashSet<BankAccount>();
            UserAccounts = new HashSet<BankAccount>();
        }
    }
}