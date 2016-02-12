using System.Web.Mvc;
using ProductCatalogueUI.Services;
using ProductCatalougeDataContract;

namespace ProductCatalogueUI.Controllers
{
    /// <summary>
    /// Class Product.
    /// </summary>
    public class ProductController : Controller
    {
        /// <summary>
        /// The _product catalogue service
        /// </summary>
        private readonly IProductCatalogueService _productCatalogueService;

        /// <summary>
        /// The _pricing service
        /// </summary>
        private readonly IPricingService _pricingService ;

        public ProductController(
            IProductCatalogueService productCatalogueService
            ,IPricingService pricingService
            )
        {
            _productCatalogueService = productCatalogueService;
            _pricingService = pricingService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Products this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Product()
        {
            var listOfProduct = _productCatalogueService.GetListOfProduct();
            return View("_product", listOfProduct);
        }

        /// <summary>
        /// Deletes the specified product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>JsonResult.</returns>
        [HttpDelete]
        public JsonResult DeleteProduct(long productId)
        {
            var isDeleted=_productCatalogueService.DeleteProduct(productId);

            if (isDeleted)
            {
                return Json(new {IsDeleted=true}, JsonRequestBehavior.AllowGet);
            }
            return Json(new {IsDeleted = false }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult AddProduct()
        {
            return PartialView("_addProduct");
        }

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns>JsonResult.</returns>
        [HttpPost]
        public JsonResult AddProduct(Product product)
        {
            _productCatalogueService.AddProduct(product);
            return Json("Product added successfully",JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the product.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult SearchProduct()
        {
            return PartialView("_searchProduct");
        }

        /// <summary>
        /// Searches the product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult SearchResultProduct(long productId)
        {
            var product = _pricingService.GetProductPrice(productId);
            return PartialView("_searchResultProduct",product);
        }
    }
}