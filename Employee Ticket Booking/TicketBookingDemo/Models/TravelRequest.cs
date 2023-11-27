using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBookingDemo.Models
{
    public class TravelRequest
    {
        [Key]
        public int RequestId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public int EmpId { get; set; } // Employee ID
        public int EmpMgrId { get; set; } // Employee Manager ID
        public string Destination { get; set; }
        public string Purpose { get; set; } 
        public DateTime DepartureDate { get; set; } 
        public DateTime ReturnDate { get; set; }
        public string TravelMode { get; set; }
        public virtual User User { get; set; }

        public DateTime RequestDate { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string CurrentStatus { get; set; }
        public string Status { get;  set; }

    }
}