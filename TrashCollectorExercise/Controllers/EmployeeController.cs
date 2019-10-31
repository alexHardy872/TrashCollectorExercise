﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorExercise.Models;
using Microsoft.AspNet.Identity;

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
            string userId = User.Identity.GetUserId();
            var employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            var employeeZip = employee.zipCode;
            var customersInZip = context.Customers.Where(c => c.zip == employeeZip).ToList();

           DateTime thisDay = DateTime.Today;
            DayOfWeek today = thisDay.DayOfWeek;

            var todaysCustomers = customersInZip.Where(c => c.pickupDay == today || c.oneTimePickup == thisDay).ToList();

            // check null start and/or end?
            var todaysAvailableCustomers = todaysCustomers.Where(c => (thisDay < c.startBreak || c.startBreak == null) || thisDay > c.endBreak).ToList();

            var todayRemaining = todaysAvailableCustomers.Where(c => c.confirmed == false).ToList();

 
            // then have react to bool confirmed and disappear AND charge customer

            return View(todayRemaining);
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
