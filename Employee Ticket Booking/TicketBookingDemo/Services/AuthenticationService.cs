using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketBookingDemo.Models;
using System.Data.Entity;

namespace TicketBookingDemo.Services
{
    public class AuthenticationService
    {
        private readonly EmployeeTicketBookingEntities dbContext;

        public AuthenticationService(EmployeeTicketBookingEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool VerifyAdminCredentials(string adminId, string adminPassword)
        {
            var admin = dbContext.AdminLogins.FirstOrDefault(a => a.AdminId == adminId && a.AdminPswd == adminPassword);
            return admin != null;
        }

        public bool VerifyEmployeeCredentials(string employeeId, string employeePassword)
        {
            var employee = dbContext.Employees.FirstOrDefault(e => e.Empid.ToString() == employeeId && e.EmpPassword.ToString() == employeePassword);
            return employee != null;
        }

        public bool VerifyManagerCredentials(string managerId, string managerPassword)
        {
            var manager = dbContext.Managers.FirstOrDefault(m => m.MgrId.ToString() == managerId && m.MgrPassword == managerPassword);
            return manager != null;
        }

        public bool VerifyAgentCredentials(string agentId, string agentPassword)
        {
            var agent = dbContext.TravelAgents.FirstOrDefault(a => a.AgentId.ToString() == agentId && a.AgentPassword == agentPassword);
            return agent != null;
        }
    }

}