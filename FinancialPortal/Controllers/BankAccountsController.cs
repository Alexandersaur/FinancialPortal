using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;
using FinancialPortal.ViewModels;
using FinancialPortal.Helpers;
using FinancialPortal.Extensions;
using System.Threading.Tasks;

namespace FinancialPortal.Controllers
{
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public ActionResult Index()
        {
            int hhId = User.Identity.GetHouseholdId().Value;
            var bankAccounts = db.BankAccounts.Include(b => b.Household).Include(b => b.Owner).Where(b => b.HouseholdId == hhId && b.IsDeleted == false);
            return View(bankAccounts.ToList());
        }


        public async Task<JsonResult> DonutMethod(int accountId)
        {
            //Initialization of data structures & lists
            string[] basecolors = new string[] { "#6993FF", "#1BC5BD", "#8950FC", "#FFA800", "#F64E60" };
            DonutChartData chartData = new DonutChartData();
            List<string> labels = new List<string>();
            List<int> series = new List<int>();
            List<string> colors = new List<string>();
            //Initialize dataset form database query of Transactions based on the account Id parameter
            List<Transaction> transactions = await db.Transactions.Include(t => t.BudgetItem).Where(t => t.AccountId == accountId).OrderBy(t => t.Id).ToListAsync();

            //Focus the dataset based on the results needed for the chart by Grouping the data
            List<ResultItem> budgetItems = transactions
                            .GroupBy(t => t.BudgetItem)
                            .Select(tb => new ResultItem     //This supplemental class helps to contain the data
                            {
                                ItemName = tb.FirstOrDefault().BudgetItem.ItemName,
                                Quantity = tb.Count(),
                                Total = tb.Sum(s => s.Amount)
                            }).ToList();
            //Control variable for varying the colors
            int colorIndex = 0;
            //Loop over data results to load data in to lists
            foreach (ResultItem result in budgetItems)
            {
                labels.Add(result.ItemName);
                series.Add(result.Quantity);
                colors.Add(basecolors[colorIndex]);
                //Manage control variable for color variance
                _ = colorIndex < basecolors.Length ? colorIndex++ : colorIndex = 0;
            }

            //Convert data lists to arrays to return to the View
            chartData.labels = labels.ToArray();
            chartData.series = series.ToArray();
            chartData.colors = colors.ToArray();
            //Return results
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccountViewModel model = new BankAccountViewModel();
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            model.BankAccount = bankAccount;
            //model.MonthlyTransactions = bankAccount.Transactions.Where(t => t.Created.Month == DateTime.Today.Month).Count();
            //model.MonthlySpending = bankAccount.Transactions.Where(t => t.TransactionType == Enums.TransactionType.Withdrawal && t.Created.Month == DateTime.Today.Month).Sum(t => t.Amount);
            //model.MonthlyDeposits = bankAccount.Transactions.Where(t => t.TransactionType == Enums.TransactionType.Deposit && t.Created.Month == DateTime.Today.Month).Sum(t => t.Amount);
            model.MonthlyTransactions = bankAccount.Transactions.Count();
            model.MonthlySpending = bankAccount.Transactions.Where(t => t.TransactionType == Enums.TransactionType.Withdrawal).Sum(t => t.Amount);
            model.MonthlyDeposits = bankAccount.Transactions.Where(t => t.TransactionType == Enums.TransactionType.Deposit).Sum(t => t.Amount);
            //model.TotalBalance = bankAccount
            //model.TotalBudget = 
            //model.TotalMonthlyDeposits = 
            //model.TotalMonthlySpending = 
            var budgetItems = db.Budgets.Where(b => b.HouseholdId == bankAccount.HouseholdId).SelectMany(b => b.Items).ToList();
            model.BudgetItemId = new SelectList(budgetItems, "Id", "ItemName");

            foreach (var item in budgetItems)
            {
                model.ApexLabels.Add(item.ItemName);
                model.ApexSeries.Add(item.Transactions.Where(t => t.TransactionType == Enums.TransactionType.Withdrawal && t.Created.Month == DateTime.Today.Month).Sum(t => t.Amount));
                //model.ApexColors.Add
            }

            return View(model);
        }



        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "HouseholdName");
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankAccount account, decimal startingBalance)
        {
            var bankAccount = new BankAccount(startingBalance, account.WarningBalance, account.AccountName);
            bankAccount.HouseholdId = account.HouseholdId;
            bankAccount.AccountType = account.AccountType;
            db.BankAccounts.Add(bankAccount);
            db.SaveChanges();
            return RedirectToAction("Index");

            //ViewBag.HouseholdId = new SelectList(db.Households, "Id", "HouseholdName", bankAccount.HouseholdId);
            //ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", bankAccount.OwnerId);
            //return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "HouseholdName", bankAccount.HouseholdId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", bankAccount.OwnerId);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,OwnerId,AccountName,Created,StartingBalance,CurrentBalance,WarningBalance,IsDeleted,AccountType")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "HouseholdName", bankAccount.HouseholdId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", bankAccount.OwnerId);
            return View(bankAccount);
        }

        public PartialViewResult _BankAccountModal()
        {
            var model = new BankAccount();
            return PartialView(model);
        }



        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            bankAccount.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Snapshot()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
