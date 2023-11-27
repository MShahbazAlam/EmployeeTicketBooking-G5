using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBookingDemo.Models;
using System.Data.Entity;
using TicketBookingDemo.Services;

namespace TicketBookingDemo.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly AuthenticationService authenticationService;
        private readonly EmployeeTicketBookingEntities dbContext;
        public LoginController()
        {
            // Initialize authentication service
            dbContext = new EmployeeTicketBookingEntities(); // Initialize your DbContext
            authenticationService = new AuthenticationService(dbContext);

        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password, string userType)
        {
            bool isAuthenticated = false;

            // Check the user type and verify credentials based on it
            switch (userType.ToLower())
            {
                case "admin":
                    isAuthenticated = authenticationService.VerifyAdminCredentials(username, password);
                    break;
                case "employee":
                    isAuthenticated = authenticationService.VerifyEmployeeCredentials(username, password);
                    break;
                case "manager":
                    isAuthenticated = authenticationService.VerifyManagerCredentials(username, password);
                    break;
                case "agent":
                    isAuthenticated = authenticationService.VerifyAgentCredentials(username, password);
                    break;
                default:
                    break;
            }

            if (isAuthenticated)
            {
                // Redirect to a respective dashboard or homepage based on user type
                switch (userType.ToLower())
                {
                    case "admin":
                        return RedirectToAction("AdminDashboard", "Admin");
                    case "employee":
                        return RedirectToAction("EmployeeDashboard", "Employee");
                    case "manager":
                        return RedirectToAction("ManagerDashboard", "Manager");
                    case "agent":
                        return RedirectToAction("Dashboard", "TravelAgent");
                    default:
                        break;
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
                return View();
            }
            ViewBag.ErrorMessage = "Invalid credentials or user type. Please try again.";
            return View();
        }


        [HttpGet]
        public ActionResult EmployeeLogin()
        {
            return View("EmployeeLoginView");
        }
       
    }
}