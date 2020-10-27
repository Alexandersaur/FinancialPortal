using FinancialPortal.Enums;
using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPortal.Extensions
{
    public static class TransactionExtensions
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        #region Notes to self while creating the class
        //What does a transaction do?
        //If TransactionType.Deposit - increases the current amount of the BankAccount it was deposited into
        //If TransactionType.Withdrawal - reduces the current amount of the BankAccount, increases the current amount of Budget and BudgetItem
        //Optional: TransactionType.Transfer - reduces current amount of BankAccount 1 and increases current amount of BankAccount 2
        #endregion

        #region Notes to future developers who would be working on this code after me
        //This code is called in the Account Controller - updated need to be made in both locations together
        #endregion

        //Chaining method calls under a public method. The three method calls in the method body will be private static void methods
        public static void UpdateBalances(this Transaction transaction)
        {
            UpdateBankBalance(transaction);

            //Deposits do not effect Budget or BudgetIte so we can test for the transaction type before calling those methods

            UpdateBudgetAmount(transaction);
            UpdateBudgetItemAmount(transaction);
        }

        public static void EditTransaction(this Transaction newTransaction, Transaction oldTransaction)
        {

        }

        private static void UpdateBankBalance(Transaction transaction)
        {
            var bankAccount = db.BankAccounts.Find(transaction.AccountId);
            //I will test the TransactionType to decide what to do
            if (transaction.TransactionType == TransactionType.Deposit)
            {
                bankAccount.CurrentBalance += transaction.Amount;
            }
            else if (transaction.TransactionType == TransactionType.Withdrawal)
            {
                bankAccount.CurrentBalance -= transaction.Amount;
            }

            db.SaveChanges();

            #region Commented Code
            //This branch accounts for a TransactionType.Transfer functionality
            //else 
            //{
            //}
            //switch (transaction.TransactionType)
            //{
            //    case TransactionType.Deposit:
            //        bankAccount.CurrentBalance += transaction.Amount;

            //        break;
            //    case TransactionType.Withdrawal:
            //        bankAccount.CurrentBalance -= transaction.Amount;

            //        break;
            //    default:
            //        return;
            //}
            #endregion
        }
        private static void UpdateBudgetAmount(Transaction transaction)
        {
            //var budget = db.Budgets.FirstOrDefault(b => b.Id == transaction.BudgetItem.BudgetId);
            var budgetItem = db.BudgetItems.Find(transaction.BudgetItemId);
            var budget = db.Budgets.Find(budgetItem?.BudgetId);
            if (budget != null)
            {
                if (transaction.TransactionType == TransactionType.Deposit)
                {
                    budget.CurrentAmount += transaction.Amount;
                }
                else if (transaction.TransactionType == TransactionType.Withdrawal)
                {
                    budget.CurrentAmount -= transaction.Amount;
                }
                db.SaveChanges();
            }
        }
        private static void UpdateBudgetItemAmount(Transaction transaction)
        {
            var budgetItem = db.BudgetItems.Find(transaction.BudgetItemId);
            if (budgetItem != null)
            {
                if (transaction.TransactionType == TransactionType.Deposit)
                {
                    budgetItem.CurrentAmount += transaction.Amount;
                }
                else if (transaction.TransactionType == TransactionType.Withdrawal)
                {
                    budgetItem.CurrentAmount -= transaction.Amount;
                }
                db.SaveChanges();
            }
        }

        //Additional functionality you need to write
        //What happens when I Edit a transaction? - Hint you might need a momento object .AsNoTracking()
        //What happens when I delete for Void a transaction - Part of these methods involve tracking the old amount and the new amount

    }
}