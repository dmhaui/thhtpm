using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLSV.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }

    }
}