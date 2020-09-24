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
    }
}