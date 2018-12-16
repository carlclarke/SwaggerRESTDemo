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
    /// Provides Customer resources
    /// </summary>
    [Route("api/v1/customer")]
    public class CustomerController : Controller
    {
        private readonly SwaggerRESTDemoContext _ctx;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        public CustomerController(SwaggerRESTDemoContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Returns a list of Customers
        /// </summary>
        /// <param name="skip">Optionally specifiy the start offset</param>
        /// <param name="take">Optionally specify how many products to return</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Customers listed", typeof(List<Customer>))]
        [HttpGet]
        public ActionResult Get([FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            List<Customer> customers = new List<Customer>();

            customers = _ctx.Customer.OrderBy(e => e.Id).Skip(skip).Take(take).ToList();

            return new OkObjectResult(customers);
        }

        /// <summary>
        /// Return a single Customer
        /// </summary>
        /// <param name="id">The Customer Id</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Customer details", typeof(Customer))]
        [SwaggerResponse(404, "Customer not found", typeof(string))]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Customer customer = _ctx.Customer.Find(id);

            if (customer != null)
            {
                return new OkObjectResult(customer);
            }
            else
            {
                return new NotFoundObjectResult("Product not found, check for valid product id");
            }
        }

        /// <summary>
        /// Create a Customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SwaggerResponse(201, "Customer created", typeof(Customer))]
        [SwaggerResponse(400, "Customer not created", typeof(Customer))]
        [HttpPost]
        public ActionResult Post([FromBody]CustomerEdit model)
        {
            Customer newCustomer = new Customer() { Title = model.Title, Firstname = model.Firstname, Lastname = model.Lastname, DateOfBirth = model.DateOfBirth, Address = model.Address, Country = model.Country, PostalCode = model.PostalCode };

            _ctx.Customer.Add(newCustomer);

            if(_ctx.SaveChanges() > 0)
            {
                return new CreatedResult($"api/customer/{newCustomer.Id}", newCustomer);
            }
            else
            {
                return new BadRequestObjectResult(model);
            }
        }

        /// <summary>
        /// Update a Customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [SwaggerResponse(200, "Customer updated", typeof(Customer))]
        [SwaggerResponse(404, "Customer not found", typeof(string))]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]CustomerEdit model)
        {
            Customer customer = _ctx.Customer.Find(id);

            if (customer != null)
            {                                                                               
                customer.Id = customer.Id; // just for clarity but we do not change it
                customer.Title = model.Title;
                customer.Firstname = model.Firstname;
                customer.Lastname = model.Lastname;
                customer.DateOfBirth = model.DateOfBirth;
                customer.Address = model.Address;
                customer.Country = model.Country;
                customer.PostalCode = model.PostalCode;

                _ctx.Customer.Update(customer);   // update like this to avoid changing pk
                _ctx.SaveChanges();

                return new OkObjectResult(customer);
            }
            else
            {
                return new NotFoundObjectResult("Customer not found, check for valid customer id");
            }
        }

        /// <summary>
        /// Delete a Customer
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(200, "Customer deleted", typeof(Customer))]
        [SwaggerResponse(404, "Customer not found", typeof(string))]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Customer customer = _ctx.Customer.Find(id);

            if (customer != null)
            {
                _ctx.Customer.Remove(customer);
                _ctx.SaveChanges();

                return new OkResult();
            }
            else
            {
                return new NotFoundObjectResult("Customer not found, check for valid product id");
            }
        }
    }
}
