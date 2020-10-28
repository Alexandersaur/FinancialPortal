using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinancialPortal.Models;

namespace FinancialPortal.Utilities
{
    public class BudgetHelper
    {
        public int GetPercentPaid(Budget budget)
        {
            if (budget.TargetAmount == 0)
            {
                return 0;
            }
            var percentPaid = (int)decimal.Round((budget.CurrentAmount / budget.TargetAmount) * 100, 0);

            if (percentPaid > 100)
            {
                percentPaid = 100;
            }
            return percentPaid;
        }

        //public int BudgetsCount()
        //{
        //    var hhId = HttpContext.Current.User.Identity.GetHouseholdId();
        //    var count = db.BankAccounts.Where(hh => hh.HouseholdId == hhId).ToList().Count;
        //    return count;
        //}

        //public List<BankAccount> ListBudgets()
        //{
        //    List<BankAccount> accounts = new List<BankAccount>();
        //    var hhId = HttpContext.Current.User.Identity.GetHouseholdId();

        //    accounts.AddRange(db.BankAccounts.Where(hh => hh.HouseholdId == hhId).ToList());

        //    return accounts;
        //}

        //public int BudgetItemsCount()
        //{
        //    var hhId = HttpContext.Current.User.Identity.GetHouseholdId();
        //    var count = db.BankAccounts.Where(hh => hh.HouseholdId == hhId).ToList().Count;
        //    return count;
        //}

        //public List<BankAccount> ListBudgetItems()
        //{
        //    List<BankAccount> accounts = new List<BankAccount>();
        //    var hhId = HttpContext.Current.User.Identity.GetHouseholdId();

        //    accounts.AddRange(db.BankAccounts.Where(hh => hh.HouseholdId == hhId).ToList());

        //    return accounts;
        //}
    }


}