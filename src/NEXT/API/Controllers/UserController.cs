using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
//using System.Web.Script.Serialization;
using Newtonsoft;
using System.IO;
using NEXT.API.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{
    [Route("api/user")]
    public class UserController : Controller
    {
        JsonSerializer serializer = new JsonSerializer();
        private ApplicationContext _context;
        public UserController(ApplicationContext context)
        {
            this._context = context;
        }

        private List<string> tempObjectStore = new List<string>();
        // GET: api/product
        [HttpGet]
        public String Get()
        {
            return JsonConvert.SerializeObject(_context.products.ToList<Product>());
        }

        // GET: api/product
        [HttpGet("{id}")]
        public string Get(int id)
        {
            IQueryable<Product> products = _context.products.Where<Product>(product => product.productID == id);
            return JsonConvert.SerializeObject(products);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromForm]string name, [FromForm]string description, [FromForm]string price, [FromForm]string body)
        {
            Product product = new Product();
            try
            {
                product.name = name;
                product.price = decimal.Parse(price);
                product.description = description;
                _context.products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] string name, [FromForm] string description, [FromForm] string price)
        {
            Product product = _context.products.Where<Product>(pSelect => pSelect.productID == id).Single();
            product.name = name;
            product.description = description;
            product.price = decimal.Parse(price);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
