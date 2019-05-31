using AbstractUniversity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractUniversityImplementList
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext() : base("UniversityDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<ClassroomCourse> GetClassroomCourses { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Study> Studies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
