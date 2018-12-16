using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwaggerRESTDemo.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SwaggerRESTDemo.Controllers
{
    /// <summary>
    /// Provides Product resources
    /// </summary>
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly SwaggerRESTDemoContext _ctx;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        public ProductController(SwaggerRESTDemoContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Returns a list of Products
        /// </summary>
        /// <param name="skip">Optionally specifiy the start offset</param>
        /// <param name="take">Optionally specify how many products to return</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Products listed", typeof(List<Product>))]
        [HttpGet]
        public ActionResult Get([FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            List<Product> products = new List<Product>();

            products = _ctx.Product.OrderBy(e => e.Id).Skip(skip).Take(take).ToList();

            return new OkObjectResult(products);
        }

        /// <summary>
        /// Return a single Product
        /// </summary>
        /// <param name="id">The Product Id</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Product details", typeof(Product))]
        [SwaggerResponse(404, "Product not found", typeof(string))]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Product product = _ctx.Product.Find(id);

            if (product != null)
            {
                return new OkObjectResult(product);
            }
            else
            {
                return new NotFoundObjectResult("Product not found, check for valid product id");
            }
        }

        /// <summary>
        /// Return a single Product with special prices
        /// </summary>
        /// <remarks>
        /// Use this to get special prices for quantity sales
        /// </remarks>
        /// <param name="id">The Product Id</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Product prices", typeof(ProductSpecialPrice))]
        [SwaggerResponse(404, "Product not found", typeof(string))]
        [Route("{id}/price")]
        [HttpGet]
        public ActionResult GetPrices(int id)
        {
            Product product = _ctx.Product.Find(id);

            if (product != null)
            {
                ProductSpecialPrice price = new ProductSpecialPrice();
                price.Product = product;

                Helpers.Pricing pricing = new Helpers.Pricing();

                price.Prices = pricing.GetProductPrices(product);

                return new OkObjectResult(price);
            }
            else
            {
                return new NotFoundObjectResult("Product not found, check for valid product id");
            }
        }

        /// <summary>
        /// Create a Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SwaggerResponse(201, "Product created", typeof(Product))]
        [SwaggerResponse(400, "Product not created", typeof(Product))]
        [HttpPost]
        public ActionResult Post([FromBody]ProductEdit model)
        {
            Product newProduct = new Product() { Description = model.Description, SalesPrice = model.SalesPrice, CostPrice = model.CostPrice };

            _ctx.Product.Add(newProduct);

            if(_ctx.SaveChanges() > 0)
            {
                return new CreatedResult($"api/product/{newProduct.Id}", newProduct);
            }
            else
            {
                return new BadRequestObjectResult(model);
            }
        }

        /// <summary>
        /// Update a Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [SwaggerResponse(200, "Product updated", typeof(Product))]
        [SwaggerResponse(404, "Product not found", typeof(string))]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]ProductEdit model)
        {
            Product product = _ctx.Product.Find(id);

            if (product != null)
            {                                                                               
                product.Id = product.Id; // just for clarity but                                                                                                                                                                                                                                                                                                                                                                                                                    n                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  zas fjk-----------/                                                                                                                                                                                                                                                                                                                we do not change it
                product.Description = model.Description;
                product.SalesPrice = model.SalesPrice;
                product.CostPrice = model.CostPrice;

                _ctx.Product.Update(product);   // update like this to avoid changing pk
                _ctx.SaveChanges();

                return new OkObjectResult(product);
            }
            else
            {
                return new NotFoundObjectResult("Product not found, check for valid product id");
            }
        }

        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(200, "Product deleted", typeof(Product))]
        [SwaggerResponse(404, "Product not found", typeof(string))]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Product product = _ctx.Product.Find(id);

            if (product != null)
            {
                _ctx.Product.Remove(product);
                _ctx.SaveChanges();

                return new OkResult();
            }
            else
            {
                return new NotFoundObjectResult("Product not found, check for valid product id");
            }
        }
    }
}
