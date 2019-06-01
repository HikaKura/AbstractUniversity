using AbstractUniversityDAL.Interface;
using AbstractUniversityImplementList;
using AbstractUniversityImplementList.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractUniversityView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, UniversityDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClassroomService, ClassroomServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITeacherService, TeacherServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudyService, StudyServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICourseService,CourseServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
