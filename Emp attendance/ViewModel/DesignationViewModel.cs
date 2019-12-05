using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpAttendance.ViewModel
{
    public class DesignationViewModel
    {
        public int Id { get; set; }

        [Required]
        public string DesignationName { get; set; }
    }
}