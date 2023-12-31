//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketBookingDemo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.TravelRequestDetails = new HashSet<TravelRequestDetail>();
        }
   
        public int Empid { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "DOB is required.")]
        public System.DateTime DOB { get; set; }

        public string Gender { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Date Of Joining is required.")]
        public System.DateTime DOJ { get; set; }
        [Required(ErrorMessage = "Employee Department  is required.")]
        public string EDeptName { get; set; }
        [Required(ErrorMessage = "Designation is required.")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Reporting Manager is required.")]
        public Nullable<int> RepMgrId { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Password Is Required !!")]
        public string EmpPassword { get; set; }
    
        public virtual Manager Manager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelRequestDetail> TravelRequestDetails { get; set; }
    }
}
