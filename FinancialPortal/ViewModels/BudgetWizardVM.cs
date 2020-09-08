using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class BudgetWizardVM
    {
        public Budget Budget { get; set; }
        public BudgetItem BudgetItems1 { get; set; }
        public BudgetItem BudgetItems2 { get; set; }
        public BudgetItem BudgetItems3 { get; set; }
    }
}