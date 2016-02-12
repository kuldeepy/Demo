using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Configuration;
using Newtonsoft.Json;
using ProductCatalougeDataContract;

namespace ProductCatalogueUI.Services
{
    /// <summary>
    /// Class ProductCatalogueService.
    /// </summary>
    public class ProductCatalogueService : IProductCatalogueService
    {
        /// <summary>
        /// The _HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The content type
        /// </summary>
        private const string ContentType = "application/json";
        private const string Accept = "application/json";


        /// <summary>
        /// The product catalogue service base URL
        /// </summary>
        private string productCatalogueServiceBaseUrl = WebConfigurationManager.AppSettings["ProductCatalogueServiceBaseUrl"];

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCatalogueService"/> class.
        /// </summary>
        public ProductCatalogueService()
        {
            _httpClient = new HttpClient();
            if (string.IsNullOrEmpty(productCatalogueServiceBaseUrl))
            {
                throw new ArgumentException("Product catalogue service base url cannot be null");
            }
        }

        public List<Product> GetListOfProduct()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(productCatalogueServiceBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = client.GetAsync("api/product").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Product>>().Result;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void AddProduct(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(productCatalogueServiceBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonProduct = JsonConvert.SerializeObject(product);
                HttpContent content = new StringContent(jsonProduct, Encoding.UTF8,"application/json");
                // New code:
                HttpResponseMessage response = client.PostAsync("api/product", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var productId= response.Content.ReadAsAsync<long>().Result;
                }
            }
        }

        public bool DeleteProduct(long productId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(productCatalogueServiceBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                HttpResponseMessage response = client.DeleteAsync("api/product?productId="+productId).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}