using System;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.IServices;
using BusinessLogicLayer.Services.UnitOfWorks;
using DAL;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using NLog;
using NLog.Interface;
using Yevhenii_KoliesnikTask1;
using Yevhenii_KoliesnikTask1.Authorization;
using Yevhenii_KoliesnikTask1.Authorization.AuthorInterface;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Yevhenii_KoliesnikTask1
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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
                kernel.Bind<System.Web.IHttpModule>().To<HttpApplicationInitializationHttpModule>();

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

           
                kernel.Bind<IGameService>().To<GameService>();
                kernel.Bind<ICommentService>().To<CommentService>();
                kernel.Bind<IReportService>().To<ReportService>();
                kernel.Bind<IGenreService>().To<GenreService>();
                kernel.Bind<IPlatformService>().To<PlatformService>();
                kernel.Bind<IPublisherService>().To<PublisherService>();
                kernel.Bind<IOrderService>().To<OrderService>();
                kernel.Bind<IShipperService>().To<ShipperService>();
                kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
                kernel.Bind<IAuthentication>().To<GameStoreAuthentication>().InRequestScope();
                kernel.Bind<GameStoreContext>().ToSelf().InRequestScope();
                kernel.Bind<NORTHWNDContext>().ToSelf().InRequestScope();
                kernel.Bind<ILanguageService>().To<LanguageService>();
                kernel.Bind<ILogger>().To<LoggerAdapter>().WithConstructorArgument("logger", x => LogManager.GetLogger(x.Request.ParentContext.Request.Service.FullName));
               
          
            
        }        
    }
}
