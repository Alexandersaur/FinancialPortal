using FinancialPortal.Extensions;
using FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialPortal.Controllers
{
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Charts
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetChartDataAjax(string type)
        {
            if (type == "Current")
            {
                var trx = db.Households.Find(User.Identity.GetHouseholdId())
                        .Accounts.SelectMany(t => t.Transactions)
                             .Where(t => t.Created.Month == DateTime.Now.Month);

                List<ChartData> data = new List<ChartData>();
                foreach (Transaction t in trx)
                {
                    data.Add(new ChartData() { x = t.BudgetItem.ItemName, y = t.Amount.ToString() });
                }

                return Json(data);
            }
            else if (type == "Last")
            {
                var trx = db.Households.Find(User.Identity.GetHouseholdId())
                        .Accounts.SelectMany(t => t.Transactions)
                             .Where(t => t.Created.Month == DateTime.Now.Month - 1);

                List<ChartData> data = new List<ChartData>();
                foreach (Transaction t in trx)
                {
                    data.Add(new ChartData() { x = t.BudgetItem.ItemName, y = t.Amount.ToString() });
                }

                return Json(data);
            }
            return null;

        }

        public JsonResult FusionDataAjax()
        {
            var trx = db.Households.Find(User.Identity.GetHouseholdId())
                                    .Accounts.SelectMany(t => t.Transactions);
            //.Where(t => t.Date.Month == DateTime.Now.Month);

            List<FusionData> data = new List<FusionData>();
            foreach (Transaction t in trx)
            {
                data.Add(new FusionData() { Label = t.BudgetItem.ItemName, Value = t.Amount.ToString() });
            }

            return Json(data);
        }

        public JsonResult AccountDataAjax()
        {
            var trx = db.Households.Find(User.Identity.GetHouseholdId())
                                    .Accounts;

            List<FusionData> data = new List<FusionData>();
            foreach (BankAccount t in trx)
            {

                data.Add(new FusionData() { Label = t.AccountName, Value = t.CurrentBalance.ToString() });
            }

            return Json(data);
        }

    }
}