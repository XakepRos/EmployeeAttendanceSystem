using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmpAttendance.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DesignationId { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [ForeignKey("DesignationId")]
        public virtual Designation DesignationDetails { get; set; }

    }
}