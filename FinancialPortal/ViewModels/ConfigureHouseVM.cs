using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class ConfigureHouseVM
    {
        //What makes a household?
        //1 or more BankAccount models
        //1 or more Budget models
        //1 or more BudgetItem models
        public int HouseholdId { get; set; }
        #region One of Everything
        //public BankAccount BankAccount { get; set; }
        //public Budget Buget { get; set; }
        //public BudgetItem BudgetItem { get; set; }
        #endregion
        #region Multiple options
        //public ICollection<BankAccount> BankAccounts { get; set; }
        //public ICollection<Budget> Budgets { get; set; }
        //public ICollection<BudgetItem> BudgetItems { get; set; }


        #endregion

        public ICollection<BankAccountWizardVM> BankAccounts { get; set; }
        public ICollection<BudgetWizardVM> Budgets { get; set; }
    }
}