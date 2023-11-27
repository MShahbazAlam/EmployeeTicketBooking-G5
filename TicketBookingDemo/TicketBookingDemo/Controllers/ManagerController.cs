using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBookingDemo.Models;
using TicketBookingDemo.Services;
using System.Data.Entity;

namespace TicketBookingDemo.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager

        private readonly EmployeeTicketBookingEntities dbContext;

        public ManagerController()
        {
            // Initialize the travel request service
            dbContext = new EmployeeTicketBookingEntities();
        }
        public ActionResult ManagerDashboard()
        {
            return View();
        }

        public ActionResult ViewRequests()
        {
            int loggedInManagerId = 20001;

            // Retrieve travel requests related to the manager
            var requestsForManager = dbContext.TravelRequestDetails
                .Where(tr => tr.EmpMgrId == loggedInManagerId)
                .ToList();

            return View(requestsForManager); 
        }


        // Action method to accept or reject a travel request
        [HttpPost]
        public ActionResult ProcessRequest(int requestId, string decision)
        {
            var request = dbContext.TravelRequestDetails.Find(requestId);

            if (request != null)
            {
                if (decision.ToLower() == "accept")
                {
                    request.ManagerStatus = "Approved"; // Update the status as approved
                    
                    request.ReadyForBooking = true;
               
                }
                else if (decision.ToLower() == "reject")
                {
                    request.ManagerStatus = "Rejected"; // Update the status as rejected
                    request.AgentStatus = "Rejected By Manager";
                }

                dbContext.SaveChanges(); // Save changes to the database
            }

            // Redirect back to the view displaying requests after processing
            return RedirectToAction("ViewRequests");
        }

        // dispose of the DbContext when the controller is disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}