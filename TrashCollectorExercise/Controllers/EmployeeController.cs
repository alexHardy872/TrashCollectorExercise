using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorExercise.Models;
using Microsoft.AspNet.Identity;
using TrashCollectorExercise.App_Start;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        public async Task<ActionResult> Details(int id)
        {

            var customer = context.Customers.Where(c => c.Id == id).Single();
            var longLat =  await GetLongLatFromApi(customer);
            // get long/ lat

            // api request

            return View(customer);
        }

        public async Task<double[]> GetLongLatFromApi(Customer customer)
        {
            //sensor=false
            API api = new API();
            double[] latLng = new double[2];
            //https://maps.googleapis.com/maps/api/geocode/json?address=11611%20W%20James%20Ave%20Franklin%20WI%2053132&key=AIzaSyCs6XucW-n-pL1f6NFCiJm0VOOuxxiB_E4
            string address = customer.streetAddress + " " + customer.city + " " + customer.state + " " + customer.zip;
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", Uri.EscapeDataString(address), api.Key);

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestUri);

            //will throw an exception if not successful
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            dynamic stuff = await Task.Run(() => JObject.Parse(content));
            var results = stuff["results"];
            var geometry = results[0]["geometry"];
            var location = geometry["location"];
            double lat = Convert.ToDouble(location["lat"]);
            double lng = Convert.ToDouble(location["lng"]);

            latLng[0] = lat;
            latLng[1] = lng;


            return latLng;
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

            ResetPastPickups(customersInZip);

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
                if (customer.confirmed == true)  // in case of reset must remove charge as well!
                {
                    customer.confirmed = false;
                    customer.balance -= Garbage.GetPricePerPickup();
                }
                
            }
            context.SaveChanges();
            return RedirectToAction("Pickups");
        }

        private void ResetPastPickups(List<Customer> customers) // TAKES OLD PICKUPS AND REVERSES THE CONFIRMATION BOOL
        {
            DateTime thisDay = DateTime.Today;
            DayOfWeek today = thisDay.DayOfWeek;
            
            if (today.Equals("Sunday"))
            {
                customers = context.Customers.Where(c => c.pickupDay > today || c.oneTimePickup != thisDay && c.pickupDay != today).ToList();
            }
            else
            {
                customers = context.Customers.Where(c => c.pickupDay < today && c.oneTimePickup != thisDay ).ToList();
                //customers = context.Customers.Where(c => c.pickupDay == today).ToList();
            }

            foreach (Customer customer in customers)
            {
                customer.confirmed = false;
            }

            context.SaveChanges();
        }

        // POST: Employee/Edit/5
     
        public ActionResult Look()
        {
            string userId = User.Identity.GetUserId();
            var employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            var employeeZip = employee.zipCode;
            var customersInZip = context.Customers.Where(c => c.zip == employeeZip).ToList();

 

            return View(customersInZip);

        }

        public ActionResult FilterByDay(DayOfWeek day)
        {
            string userId = User.Identity.GetUserId();
            var employee = context.Employees.Where(e => e.ApplicationId == userId).Single();
            var employeeZip = employee.zipCode;
            var customersInZip = context.Customers.Where(c => c.zip == employeeZip).ToList();
            var customersInZipFiltered = customersInZip.Where(c => c.pickupDay == day).ToList();
            return View(customersInZipFiltered);

        }



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
