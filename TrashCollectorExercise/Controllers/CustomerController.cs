using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorExercise.Models;
using Microsoft.AspNet.Identity;
using TrashCollectorExercise.ViewModels;

namespace TrashCollectorExercise.Controllers
{
   // [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        ApplicationDbContext context;

        public CustomerController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Customer   HOMEPAGE, LIST OF CUSTOMER DETAILS IN LIST FORM
        public ActionResult Index()
        {

            string userId = User.Identity.GetUserId();
            var customer = context.Customers.Where(h => h.ApplicationId == userId).ToList();
            return View(customer);
              
        }

        // GET: Customer/Details/5
        public ActionResult Details()//int id)
        {
            string userId = User.Identity.GetUserId();
            var customer = context.Customers.Where(h => h.ApplicationId == userId).FirstOrDefault();
            return View(customer);
        }

        public ActionResult Balance()
        {
            string userId = User.Identity.GetUserId();
            var customer = context.Customers.Where(h => h.ApplicationId == userId).FirstOrDefault();

            BalanceViewModel balanceView = new BalanceViewModel();

            balanceView.customer = customer;
            balanceView.predictedBalance = PredictBillForMonth(customer);


            return View(balanceView);
        }

        private string PredictBillForMonth(Customer customer)
        {
            var price = App_Start.Garbage.GetPricePerPickup();
            DateTime thisDay = DateTime.Now;
            var month = thisDay.Month;
            var year = thisDay.Year;
            var daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime date = thisDay;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            List<DateTime> billedDatesOfMonth = new List<DateTime>();
            for (int i = 1; i < daysInMonth + 1; i++) // logic for suspension
            {
                DateTime newDay = new DateTime(year, month, i);
                if (newDay.DayOfWeek == customer.pickupDay || newDay == customer.oneTimePickup)
                {
                    if (customer.startBreak <= newDay && newDay <= customer.endBreak)
                    {

                    }
                    else
                    {
                        billedDatesOfMonth.Add(newDay);
                    }
                }
            }
            var predictedPickups = billedDatesOfMonth.Count;
            decimal predictedBill = predictedPickups * price;
            var FormatBill = string.Format("{0:C}", predictedBill);
            return FormatBill;
        }


        // GET: Customer/Create
        public ActionResult Create()
        {
            
            Customer customer = new Customer();
          
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                customer.ApplicationId = userId;
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit() 
        {
            string userId = User.Identity.GetUserId();
            Customer customerInDb = context.Customers.Single(m => m.ApplicationId == userId);
            return View(customerInDb);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                var customerInDb = context.Customers.Single(m => m.Id == customer.Id);
                customerInDb.firstName = customer.firstName;
                customerInDb.lastName = customer.lastName;
                customerInDb.streetAddress = customer.streetAddress;
                customerInDb.city = customer.city;
                customerInDb.state = customer.state;
                customerInDb.zip = customer.zip;
                customerInDb.pickupDay = customer.pickupDay;
                customerInDb.balance = customer.balance;
                customerInDb.startBreak = customer.startBreak;
                customerInDb.endBreak = customer.endBreak;
                customerInDb.oneTimePickup = customer.oneTimePickup;
                customerInDb.ApplicationId = userId;

                context.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }



       


        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
