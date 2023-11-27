using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketBookingDemo.Helpers
{
    public class DropDownHelper
    {
     
        public static IEnumerable<SelectListItem> GetTypes()
        {
            // Logic to retrieve user types and convert them to SelectListItem
            var userTypes = new List<string> { "Admin", "Employee", "Manager", "Agent" };

            var selectListItems = new List<SelectListItem>();
            foreach (var userType in userTypes)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = userType,
                    Value = userType
                });
            }

            return selectListItems;
        }
    }
}