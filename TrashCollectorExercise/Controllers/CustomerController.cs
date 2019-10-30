using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorExercise.Models;
using Microsoft.AspNet.Identity;

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
        // GET: Customer
        public ActionResult Index()
        {
            return View();   
        }

        // GET: Customer/Details/5
        public ActionResult Details()//int id)
        {
            string userId = User.Identity.GetUserId();
            var customer = context.Customers.Where(h => h.ApplicationId == userId).FirstOrDefault();
            return View(customer);
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
                return RedirectToAction("Index");
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
