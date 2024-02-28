using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace QLSV.Models
{
    public class CSDLContext: DbContext
    {
        public CSDLContext():base("Data Source=MINH-MINH; Initial Catalog=CSDL; User ID=abc; Password=123456") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public virtual DbSet<Student> Students { get; set; }
    }
}