using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ProductCatalogueUI.Windsor;

namespace ProductCatalogueUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IWindsorContainer _container;

        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ConfigureWindsorConfig();
        }

        /// <summary>
        /// Configures the windsor configuration.
        /// </summary>
        private void ConfigureWindsorConfig()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            //GlobalConfiguration.Configuration.Services.Replace(
            //    typeof(DefaultControllerFactory),
            //    new WindsorCompositionRoot(_container.Kernel));
            var controllerFactory = new WindsorCompositionRoot(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
            var dependencyResolver = new WindsorDependencyResolver(_container);
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
        }
    }
}
