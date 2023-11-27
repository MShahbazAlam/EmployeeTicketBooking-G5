using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketBookingDemo.Models;
using TicketBookingDemo.Services;

namespace TicketBookingDemo.Controllers
{
    public class AdminController : Controller
    {
     
        private readonly EmployeeTicketBookingEntities dbContext;
        public AdminController()
        {
            dbContext = new EmployeeTicketBookingEntities(); // Initialize your DbContext

        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
                                                                   //Employee
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new EmployeeTicketBookingEntities())
                {
                    // Check for NOT NULL constraints and UNIQUE constraints

                    // Check for Name NOT NULL constraint
                    if (string.IsNullOrEmpty(employee.Name))
                    {
                        ModelState.AddModelError("Name", "Name is required.");
                    }

                    // Check for DOB NOT NULL constraint
                    if (employee.DOB == null || employee.DOB == DateTime.MinValue)
                    {
                        ModelState.AddModelError("DOB", "DOB is required.");
                    }

                    // Check for Email NOT NULL and UNIQUE constraints
                    if (string.IsNullOrEmpty(employee.Email))
                    {
                        ModelState.AddModelError("Email", "Email is required.");
                    }
                    else
                    {
                        bool isEmailUnique = !dbContext.Employees.Any(e => e.Email == employee.Email);

                        if (!isEmailUnique)
                        {
                            ModelState.AddModelError("Email", "Email already exists.");
                        }
                    }

                    // Check for PhoneNumber NOT NULL and UNIQUE constraints
                    if (string.IsNullOrEmpty(employee.PhoneNumber))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone number is required.");
                    }
                    else
                    {
                        bool isPhoneNumberUnique = !dbContext.Employees.Any(e => e.PhoneNumber == employee.PhoneNumber);

                        if (!isPhoneNumberUnique)
                        {
                            ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                        }
                    }

                    // Check for DOJ NOT NULL constraint
                    if (employee.DOJ == null || employee.DOJ == DateTime.MinValue)
                    {
                        ModelState.AddModelError("DOJ", "DOJ is required.");
                    }

                    // Check for EDeptName NOT NULL constraint
                    if (string.IsNullOrEmpty(employee.EDeptName))
                    {
                        ModelState.AddModelError("EDeptName", "EDeptName is required.");
                    }

                    // Check for RepMgrId NOT NULL constraint
                    if (employee.RepMgrId == null)
                    {
                        ModelState.AddModelError("RepMgrId", "RepMgrId is required.");
                    }
                    var managerExists = dbContext.Managers.Any(m => m.MgrId == employee.RepMgrId);

                    if (!managerExists)
                    {
                        ModelState.AddModelError("RepMgrId", "Manager does not exist. Please provide a valid Manager ID.");
                        return View(employee);
                    }

                    if (ModelState.IsValid) // If there are no model errors, proceed to save
                    {
                        dbContext.Employees.Add(employee);
                        dbContext.SaveChanges();
                        return RedirectToAction("EmployeeList");
                    }
                }
            }

            return View(employee);
        }

        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(employee).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("EmployeeList");
            }

            return View(employee);
        }

        public ActionResult DeleteEmployee(int id)
        {
            // Retrieve the agent to be deleted
            var employeeToDelete = dbContext.Employees.FirstOrDefault(a => a.Empid == id);

            if (employeeToDelete != null)
            {
                // Remove the agent from the database
                dbContext.Employees.Remove(employeeToDelete);
                dbContext.SaveChanges();

                // Redirect to a page or action method after deletion (e.g., Agent list page)
                return RedirectToAction("EmployeeList");
            }
            else
            {
                // Agent not found, handle the error (display a message or redirect to an error page)
                return RedirectToAction("EmployeeList"); // Redirect back to the Agent list page
            }
        }



        //// Action to display the list of employees
        public ActionResult EmployeeList()
        {
            
            using (var dbContext = new EmployeeTicketBookingEntities())
            {
                var emp = dbContext.Employees.ToList();
                return View(emp); // Return the list to the view
            }
        }
                                                                //Agent

        // Action to display the Add Travel Agent view
        public ActionResult AddTravelAgent()
        {
            return View();
        }

        // POST action to handle adding a new travel agent
        [HttpPost]
        public ActionResult AddTravelAgent(TravelAgent travelAgent)
        {
            if (ModelState.IsValid)
            {
                // Add the travel agent data to the database
                using (var dbContext = new EmployeeTicketBookingEntities())
                {
                    dbContext.TravelAgents.Add(travelAgent);
                    dbContext.SaveChanges();
                }

                // Redirect to a success page or another action after adding the travel agent
                return RedirectToAction("TravelAgentList"); // Redirect to the list of travel agents or another appropriate action
            }

            // If ModelState is not valid, return the view with errors
            return View(travelAgent);
        }

        

        // Action to display the Edit Travel Agent view 
       
        public ActionResult EditTravelAgent(int id)
        {
            var travelAgent = dbContext.TravelAgents.Find(id);

            if (travelAgent == null)
            {
                return HttpNotFound(); // Or a custom error view if needed
            }

            return View(travelAgent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTravelAgent(TravelAgent updatedTravelAgent)
        {
            if (ModelState.IsValid)
            {
                var existingTravelAgent = dbContext.TravelAgents.Find(updatedTravelAgent.AgentId);

                if (existingTravelAgent == null)
                {
                    return HttpNotFound(); // Or a custom error view if needed
                }
                existingTravelAgent.AgentId = updatedTravelAgent.AgentId;
                existingTravelAgent.Name = updatedTravelAgent.Name;
                existingTravelAgent.Email = updatedTravelAgent.Email;
                existingTravelAgent.PhoneNumber = updatedTravelAgent.PhoneNumber;
                existingTravelAgent.Agencyid = updatedTravelAgent.Agencyid;
                existingTravelAgent.AgentPassword = updatedTravelAgent.AgentPassword;

                dbContext.SaveChanges();

                return RedirectToAction("TravelAgentList"); // Redirect to the list of travel agents after successful update
            }

            return View(updatedTravelAgent); // Return the same view with errors if the model state is invalid
        }

        //Delete Agent
        public ActionResult DeleteTravelAgent(int id)
        {
            // Retrieve the agent to be deleted
            var agentToDelete = dbContext.TravelAgents.FirstOrDefault(a => a.AgentId == id);

            if (agentToDelete != null)
            {
                // Remove the agent from the database
                dbContext.TravelAgents.Remove(agentToDelete);
                dbContext.SaveChanges();

                // Redirect to a page or action method after deletion (e.g., Agent list page)
                return RedirectToAction("TravelAgentList");
            }
            else
            {
                // Agent not found, handle the error (display a message or redirect to an error page)
                return RedirectToAction("TravelAgentList"); // Redirect back to the Agent list page
            }
        }


        // Action to display the list of travel agents
        public ActionResult TravelAgentList()
        {
             // Fetch all travel 
            using (var dbContext = new EmployeeTicketBookingEntities())
            {
                var travelAgents = dbContext.TravelAgents.ToList();
                return View(travelAgents); // Return the list to the view
            }
        }

                                                // MAnager
       
        // Action to display the Add Manager view
        public ActionResult AddManager()
        {
            
            return View();
        }

       
        [HttpPost]
        public ActionResult AddManager(Manager manager)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new EmployeeTicketBookingEntities())
                {
                    if (string.IsNullOrEmpty(manager.Email))
                    {
                        ModelState.AddModelError("Email", "Email is required.");
                    }
                    else
                    {
                        bool isEmailUnique = !dbContext.Employees.Any(e => e.Email == manager.Email);

                        if (!isEmailUnique)
                        {
                            ModelState.AddModelError("Email", "Email already exists.");
                        }
                    }

                    // Check for PhoneNumber NOT NULL and UNIQUE constraints
                    if (string.IsNullOrEmpty(manager.PhoneNumber))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone number is required.");
                    }
                    else
                    {
                        bool isPhoneNumberUnique = !dbContext.Employees.Any(e => e.PhoneNumber == manager.PhoneNumber);

                        if (!isPhoneNumberUnique)
                        {
                            ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                        }
                    }

                    if (ModelState.IsValid) // If there are no model errors, proceed to save
                    {
                        dbContext.Managers.Add(manager);
                        dbContext.SaveChanges();
                        return RedirectToAction("EmployeeList");
                    }
                }
            }
            return View(manager); // Return the view with the model in case of validation errors
        }

        [HttpGet]
        public ActionResult EditManager(int id)
        {
            var manager = dbContext.Managers.Find(id);

            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        [HttpPost]
        public ActionResult EditManager(Manager manager)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(manager).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("ManagerList");
            }

            return View(manager);
        }

        public ActionResult DeleteManager(int id)
        {
            // Retrieve the manager to be deleted
            var managerToDelete = dbContext.Managers.FirstOrDefault(a => a.MgrId == id);

            if (managerToDelete != null)
            {
                // Remove the manager from the database
                dbContext.Managers.Remove(managerToDelete);
                dbContext.SaveChanges();

                // Redirect to a page or action method after deletion 
                return RedirectToAction("ManagerList");
            }
            else
            {
                // Agent not found, handle the error (display a message or redirect to an error page)
                return RedirectToAction("ManagerList"); // Redirect back to the Agent list page
            }
        }

        // Action to display the list of managers
        public ActionResult Managerlist()
        {
            using (var dbContext = new EmployeeTicketBookingEntities())
            {
                var managers = dbContext.Managers.ToList();
                return View(managers); // Return the list to the view
            }
        }
        

    }
}