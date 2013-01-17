using System.Web.Http;
using System;
using System.Web;
using System.Web.Routing;
using AwesomeSPAReboot.App_Start.IoC;
using AwesomeSPAReboot.Infrastructure;
using AwesomeSPAReboot.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;

[assembly: WebActivator.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace AwesomeSPAReboot.App_Start.IoC
{
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
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            //kernel.Bind(x => x
            // .FromAssembliesMatching("*")
            // .SelectAllClasses()
            // .BindDefaultInterface());


            // Tell WebApi how to use our Ninject IoC
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            var signalrDependencyResolver = new SignalrDependencyResolver(kernel);
            GlobalHost.DependencyResolver = signalrDependencyResolver;
            RouteTable.Routes.MapHubs(signalrDependencyResolver);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load<RavenDBNinjectModule>();
            kernel.Bind<UpdateHub>().ToSelf().InRequestScope();
            kernel.Bind<ISearchRepository>().To<SearchRepository>();
            kernel.Bind<IImagesService>().To<ImagesService>();
            kernel.Bind<IConfigurationProvider>().To<ConfigurationProvider>().InSingletonScope();
        }        
    }
}
