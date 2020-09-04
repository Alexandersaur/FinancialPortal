using FinancialPortal.Enums;
using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.ViewModels
{
    public class BankAccountWizardVM
    {
        public decimal StartingBalance { get; set; }
        public decimal WarningBalance { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
    }
}