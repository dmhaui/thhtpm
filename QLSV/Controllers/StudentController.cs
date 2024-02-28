using QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLSV.Controllers
{
    public class StudentController : ApiController
    {
        public List<Student> Get()
        {
            using(CSDLContext db = new CSDLContext())
            {
                return db.Students.ToList();
            }
        }
         public IHttpActionResult Post([FromBody]Student student)
        {
            using (CSDLContext db = new CSDLContext())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
            return Ok();
        }
        public IHttpActionResult Put([FromBody] Student student)
        {
            using (CSDLContext db = new CSDLContext())
            {
                Student student1 = db.Students.Find(student.ID);
                if (student1 != null)
                {
                    student1.Code = student.Code;
                    student1.FullName = student.FullName;
                    student1.BirthDate = student.BirthDate;
                    student1.Gender = student.Gender;
                    db.SaveChanges();
                }
                else
                    return NotFound();
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            using (CSDLContext db = new CSDLContext())
            {
                Student student = db.Students.Find(id);
                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                    return Ok();
                }
                else
                    return NotFound();
            }          
        }
    }
}
