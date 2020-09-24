using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialPortal.Models;

namespace FinancialPortal.ViewModels
{
    public class HouseholdDashboardVM
    {
        public ICollection<Budget> HouseBudgets { get; set; }

        public ICollection<BankAccount> HouseAccounts { get; set; }

        public ICollection<ApplicationUser> HouseUsers { get; set; }

        public Household Household { get; set; }

        public HouseholdDashboardVM()
        {
            HouseBudgets = new HashSet<Budget>();
            HouseAccounts = new HashSet<BankAccount>();
            HouseUsers = new HashSet<ApplicationUser>();
        }
    }
}