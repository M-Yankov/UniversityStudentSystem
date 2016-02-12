[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UniversityStudentSystem.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UniversityStudentSystem.Web.NinjectWebCommon), "Stop")]

namespace UniversityStudentSystem.Web
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Data;
    using System.Data.Entity;
    using Data.Repositories;
    using Data.Models;
    using Data.Models.CommonModels;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            
            kernel.Bind<DbContext>().To<UniversityDbContext>().InRequestScope();
            // <> to <,int>
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));

            kernel.Bind(typeof(IRepository<,>)).To(typeof(EfRepository<,>));

            var h = kernel.GetBindings(typeof(IRepository<>));

            kernel.Bind(b => b.From("UniversityStudentSystem.Services").SelectAllClasses().BindDefaultInterfaces());
        }        
    }
}
