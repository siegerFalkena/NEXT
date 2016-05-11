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
using NEXT.API.Repositories;
using NEXT.API.Query;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{
    [Route("api/product")]
    public class ProductController : Controller
    {

        JsonSerializer serializer = new JsonSerializer();
        private NEXTContext _context;
        private IProductRepository productRepo;
        private IProductTypeRepository typeRepo;
        private IBrandRepository brandRepo;


        public ProductController(NEXTContext context, IProductRepository productRepo, IProductTypeRepository typeRepo, IBrandRepository brandRepo)
        {
            this.brandRepo = brandRepo;
            this._context = context;
            this.productRepo = productRepo;
            this.typeRepo = typeRepo;
        }

        // GET: api/category
        private static int defaultPage = 0;
        private static int defaultPageResults = 25;


        [HttpGet]
        public String Get([FromQuery][Bind("min_Created,max_Created,CreatedBy,ExternalProductIdentifier,min_LastModified,max_LastModified,LastModifiedBy,ParentProductID,ProductTypeID,SKU")]ProductQuery query, [FromQuery]int page, [FromQuery]int results)
        {
            return JsonConvert.SerializeObject(productRepo.getProducts(query, page, results));
        }

        // GET: api/product
        [HttpGet("{id}")]
        public string Get(int id)
        {
            IQueryable<Product> products = _context.Product.Where<Product>(product => product.ID == id);
            return JsonConvert.SerializeObject(products);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromForm][Bind("SKU,BrandID,Created,CreatedBy,ExternalProductIdentifier,LastModified,LastModifiedBy,ParentProductID,ProductTypeID", Prefix = "p")] Product product,
                         [FromForm][Bind("Name,ID", Prefix = "b")]Brand newBrand,
                         [FromForm][Bind("Name,ID", Prefix = "t")]ProductType newType)
        {
            if (ModelState.IsValid)
            {
                productRepo.insertProduct(product, newType, newBrand);
                productRepo.Save();
            }
            else {
                Response.StatusCode = 400;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] string name, [FromForm] string description, [FromForm] string price)
        {
            Product product = _context.Product.Where<Product>(pSelect => pSelect.ID == id).Single();
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
