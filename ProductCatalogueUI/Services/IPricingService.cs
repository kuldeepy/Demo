using ProductCatalougeDataContract;

namespace ProductCatalogueUI.Services
{
    /// <summary>
    /// Interface IPricingService
    /// </summary>
    public interface IPricingService
    {
        /// <summary>
        /// Gets the product price.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Product.</returns>
        Product GetProductPrice(long productId);
    }
}
