using AbstractUniversity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AbstractUniversityImplementList
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext() : base("UniversityDb")
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
        public virtual DbSet<ClassroomCourse> ClassroomCourses { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Study> Studies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<AbstractUniversityDAL.BindingModel.StudyBindingModel> StudyBindingModels { get; set; }
    }
}
