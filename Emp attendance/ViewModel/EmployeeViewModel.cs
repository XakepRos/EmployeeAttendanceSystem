using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpAttendance.ViewModel
{
    public class EmployeeViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DesignationId { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Designation { get; set; }
        public int Age { get; set; }
    }
}