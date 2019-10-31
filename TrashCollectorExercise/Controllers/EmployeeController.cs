﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorExercise.Models;
using Microsoft.AspNet.Identity;
using TrashCollectorExercise.App_Start;

namespace TrashCollectorExercise.Controllers
{

    //[Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        ApplicationDbContext context;

        public EmployeeController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Employee
        public ActionResult Index()
        {

            return RedirectToAction("Pickups");
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {

            var customer = context.Customers.Where(c => c.Id == id).Single();
            return View(customer);
        }


        // GET: Employee/Create
        public ActionResult Create()
        {

            Employee employee = new Employee();

            return View(employee);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                employee.ApplicationId = userId;
                context.Employees.Add(employee);
                context.SaveChanges();
                return RedirectToAction("Pickups");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Pickups()
        {
            ResetPastPickups();
            string userId = User.Identity.GetUserId();
            var employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            var employeeZip = employee.zipCode;
            var customersInZip = context.Customers.Where(c => c.zip == employeeZip).ToList();

           DateTime thisDay = DateTime.Today;
            DayOfWeek today = thisDay.DayOfWeek;

            var todaysCustomers = customersInZip.Where(c => c.pickupDay == today || c.oneTimePickup == thisDay).ToList();

            // check null start and/or end?
            var todaysAvailableCustomers = todaysCustomers.Where(c => (thisDay < c.startBreak || c.startBreak == null) || (thisDay > c.endBreak || c.endBreak == null)).ToList();

            var todayRemaining = todaysAvailableCustomers.Where(c => c.confirmed == false).ToList();

 
            // then have react to bool confirmed and disappear AND charge customer

            return View(todayRemaining);
        }
        public ActionResult UpdateList(int id)
        {
           var customer =  context.Customers.Where(c => c.Id == id).Single();
            customer.confirmed = true;
            customer.balance += Garbage.GetPricePerPickup();
            context.SaveChanges();
            return RedirectToAction("Pickups");

        }

        public ActionResult ResetList()    // RESETS LIST FOR THE DAY BY MAKING CONFIRMED FALSE
        {
            string userId = User.Identity.GetUserId();
            var employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            var employeeZip = employee.zipCode;
            var customersInZip = context.Customers.Where(c => c.zip == employeeZip).ToList();
            foreach (Customer customer in customersInZip)
            {
                customer.confirmed = false;
            }
            context.SaveChanges();
            return RedirectToAction("Pickups");
        }

        private void ResetPastPickups() // TAKES OLD PICKUPS AND REVERSES THE CONFIRMATION BOOL
        {
            DateTime thisDay = DateTime.Today;
            DayOfWeek today = thisDay.DayOfWeek;
            var customers = new List<Customer>();
            if (today.Equals("Sunday"))
            {
                customers = context.Customers.Where(c => c.pickupDay > today || c.oneTimePickup < thisDay && c.pickupDay != today).ToList();
            }
            else
            {
                customers = context.Customers.Where(c => c.pickupDay < today || c.oneTimePickup < thisDay && c.pickupDay != today).ToList();
                //customers = context.Customers.Where(c => c.pickupDay == today).ToList();
            }




            foreach (Customer customer in customers)
            {
                customer.confirmed = false;
            }

            context.SaveChanges();
        }

        // POST: Employee/Edit/5
     

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
