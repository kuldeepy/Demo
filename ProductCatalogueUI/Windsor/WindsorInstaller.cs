using System.Web.Http.Controllers;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ProductCatalogueUI.Services;

namespace ProductCatalogueUI.Windsor
{
    /// <summary>
    /// Class WindsorInstaller.
    /// </summary>
    public class WindsorInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());
            container.Register(Component.For<IPricingService>().ImplementedBy<PricingService>().LifestylePerWebRequest());  
            container.Register(Component.For<IProductCatalogueService>().ImplementedBy<ProductCatalogueService>().LifestylePerWebRequest());
        }
    }
}   