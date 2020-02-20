using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingWeb.Models;

namespace AccountingWeb.Controllers
{
    public class BudgetController : Controller
    {
        public ActionResult Index()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Create(Budgets budgets)
        {
            int res = 0;
            if (!Int32.TryParse(budgets.YearMonth, out res))
            {
                ViewBag.Message = "YearMonth is Error";
                return View();
            }

            String msg = "";
            using (AccountingEntities ent = new AccountingEntities())
            {
                var record = ent.Budgets.FirstOrDefault(x => x.YearMonth.Equals(budgets.YearMonth));
                if (record != null)
                {
                    record.Amount = budgets.Amount;
                    msg = "Update OK";
                }
                else
                {
                    msg = "Add OK";
                }

                ent.Budgets.Add(budgets);
                ent.SaveChanges();
            }

            ViewBag.Message = msg;
            return View();

        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}