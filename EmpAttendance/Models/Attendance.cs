using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmpAttendance.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime InTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime OutTime { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("EmpId")]
        public virtual Employee EmployeeDetails { get; set; }
    }
}