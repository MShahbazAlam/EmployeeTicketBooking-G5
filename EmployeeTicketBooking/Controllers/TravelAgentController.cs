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
    public class TravelAgentController : Controller
    {
        private readonly EmployeeTicketBookingEntities dbContext = new EmployeeTicketBookingEntities(); // Initialize your DbContext

        // Dashboard for the travel agent to view requests
        public ActionResult Dashboard()
        {
            var requests = dbContext.TravelRequestDetails.ToList();
            return View(requests);
        }

        // Action method for handling booking or cancellation
        [HttpPost]
        public ActionResult HandleBooking(int requestId, string action)
        {
            var request = dbContext.TravelRequestDetails.Find(requestId);

            if (request != null)
            {
                if (action.ToLower() == "book")
                {
                    // Perform booking logic
                    request.AgentStatus = "Booked";
                    dbContext.SaveChanges();
                }
                else if (action.ToLower() == "cancel")
                {
                    // Perform cancellation logic
                    request.AgentStatus = "Canceled";
                    dbContext.SaveChanges();
                }
            }

            return RedirectToAction("Dashboard");
        }
        
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