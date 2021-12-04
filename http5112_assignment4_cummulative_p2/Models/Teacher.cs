using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace http5112_assignment4_cummulative_p2.Models
{
    public class Teacher
    {
        //model defines the structure of the application.
        //teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary
        public int teacherId { get; set; }
        public String teacherFName { get; set; }
        public String teachetLName { get; set; }
        public String employeeNumber { get; set; }
        public String hiredate { get; set; }
        public decimal salary { get; set; }
    }
}