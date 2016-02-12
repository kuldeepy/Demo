using System.Collections.Generic;
using ProductCatalougeDataContract;

namespace ProductCatalogueUI.Services
{
    /// <summary>
    /// Interface IProductCatalogueService
    /// </summary>
    public interface IProductCatalogueService
    {
        /// <summary>
        /// Gets the list of product.
        /// </summary>
        /// <returns>List&lt;ProductCatalougeDataContract.Product&gt;.</returns>
        List<ProductCatalougeDataContract.Product> GetListOfProduct();

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void AddProduct(Product product);

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        bool DeleteProduct(long productId);

    }
}
