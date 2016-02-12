using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using ProductCatalougeDataContract;

namespace ProductCatalogueUI.Services
{
    /// <summary>
    /// Class PricingService.
    /// </summary>
    public class PricingService : IPricingService
    {
        /// <summary>
        /// The product catalogue service base URL
        /// </summary>
        private string productCatalogueServiceBaseUrl = WebConfigurationManager.AppSettings["PricingServiceBaseUrl"];

        /// <summary>
        /// Gets the product price.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>Product.</returns>
        public Product GetProductPrice(long productId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(productCatalogueServiceBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = client.GetAsync("api/pricing?productId="+productId).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Product>().Result;
                }
            }

            return null;
        }
    }
}