using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBookingDemo.Models;
using TicketBookingDemo.Services;


namespace TicketBookingDemo.Controllers
{
    public class EmployeeController : Controller
    {
       
        public ActionResult EmployeeDashboard()
        {
            return View();
        }

        // GET: Employee
        public ActionResult RequestTravel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RequestTravel(TravelRequestDetail travelRequest)
        {
            if (ModelState.IsValid)
            {
                // Save the travel request to the database
                using (var dbContext = new EmployeeTicketBookingEntities())
                {
                    if (travelRequest.EmpMgrId == null)
                    {
                        ModelState.AddModelError("RepMgrId", "RepMgrId is required.");
                    }
                    var managerExists = dbContext.Managers.Any(m => m.MgrId == travelRequest.EmpMgrId);

                    if (!managerExists)
                    {
                        ModelState.AddModelError("RepMgrId", "Manager does not exist. Please provide a valid Manager ID.");
                        return View(travelRequest);
                    }

                    if (travelRequest.EmpId == null)
                    {
                        ModelState.AddModelError("RepMgrId", "RepMgrId is required.");
                    }
                    var employeeExists = dbContext.Employees.Any(m => m.Empid == travelRequest.EmpId);

                    if (!employeeExists)
                    {
                        ModelState.AddModelError("RepMgrId", "Manager does not exist. Please provide a valid Manager ID.");
                        return View(travelRequest);
                    }
                    if (ModelState.IsValid) // If there are no model errors, proceed to save
                    {
                        dbContext.TravelRequestDetails.Add(travelRequest);
                        dbContext.SaveChanges();
                        return RedirectToAction("EmployeeList"); // Redirect to view requests page or another appropriate action
                    }

                }

            }
            return View(travelRequest);
        }

        public ActionResult TravelList()
        {
            using (var dbContext = new EmployeeTicketBookingEntities())
            {
                var travellist = dbContext.TravelRequestDetails.ToList();
                return View(travellist); // Return the list to the view
            }
        }

        public ActionResult TrackTravel()
        {
            using (var dbContext = new EmployeeTicketBookingEntities())
            {
                var travellist = dbContext.TravelRequestDetails.ToList();
                return View(travellist); // Return the list to the view
            }
        }
  
    }
}