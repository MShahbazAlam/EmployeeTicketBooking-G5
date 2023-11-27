using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace TicketBookingDemo.Models
{
    public class UserType
    {
        public static UserType Employee { get; set; }
        public static UserType Manager { get; set; }
        public static UserType Agent { get; internal set; }

        public int UserTypeId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}