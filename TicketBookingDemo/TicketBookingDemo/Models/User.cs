using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingDemo.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "User Type is required")]
        public UserType UserType { get; set; }


        [Key]
        public int EmpId { get; set; }

        [Key]
        public int MgrId { get; set; }


        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [MaxLength(15)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of Joining is required")]
        [DataType(DataType.Date)]
        public DateTime DOJ { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public string DeptName { get; set; }

        [MaxLength]
        public string Designation { get; set; }

        public int RepMgrId { get; set; }

        [MaxLength(15)]
        public string MaritalStatus { get; set; }

        [MaxLength]
        public string Address { get; set; }

        // Additional property for password
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100)] // Adjust as needed
        [DataType(DataType.Password)]
        public string EmpPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100)] // Adjust as needed
        [DataType(DataType.Password)]
        public string MgrPassword { get; set; }

    }
}