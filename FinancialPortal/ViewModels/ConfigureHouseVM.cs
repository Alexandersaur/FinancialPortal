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
        public int? HouseholdId { get; set; }
        #region One of Everything
        public BankAccount BankAccount { get; set; }
        public decimal StartingBalance { get; set; }
        public Budget Budget { get; set; }
        public BudgetItem BudgetItem { get; set; }
        #endregion

        #region Multiple options
        //public ICollection<BankAccount> BankAccounts { get; set; }
        //public ICollection<Budget> Budgets { get; set; }
        //public ICollection<BudgetItem> BudgetItems { get; set; }
        #endregion

        #region Most complex
        //public ICollection<BankAccountWizardVM> BankAccounts { get; set; }
        //public ICollection<BudgetWizardVM> Budgets { get; set; }
        #endregion

        //set up one checking and one savings account
        //public BankAccountWizardVM BankAccount1 { get; set; }
        //public BankAccountWizardVM BankAccount2 { get; set; }

        //public BudgetWizardVM Budget1 { get; set; }
        //public BudgetWizardVM Budget2 { get; set; }
        //public BudgetWizardVM Budget3 { get; set; }
    }
}