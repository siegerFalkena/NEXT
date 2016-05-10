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
        public String Get([FromQuery]string page, [FromQuery]string results, [FromQuery]string name, [FromQuery]string fpricemin, [FromQuery]string fpricemax)
        {

            return JsonConvert.SerializeObject(null);
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
        public void Post([FromForm] string SKU, [FromForm] int brandID, [FromForm] string brandName, [FromForm] int productTypeID, [FromForm] string productTypeName, [FromForm] int parentProductID, [FromForm] string externalProductIdentifier, [FromForm] string created, [FromForm] int createdBy)
        //http://localhost:58895/api/product?SKU=product&brandName=brandName&productTypeName=typename&externalProductIdentifier=productIdentifyer&created=5/1/2008 8:30:52 AM&createdBy=1
        {
            Product product = new Product();
            Brand brand;
            if (brandID == 0 && brandName != null)
            {
                brand = new Brand();
                brand.Name = brandName;
            }
            else
            {
                brand = brandRepo.getBrandByID(brandID);
            }

            ProductType type;
            if (productTypeID == 0 && productTypeName != null)
            {
                type = new ProductType();
                type.Name = productTypeName;
            }
            else
            {
                type = typeRepo.getProductTypeByID(productTypeID);
            }

            product.ProductType = type;
            product.Brand = brand;

            product.SKU = SKU;
            product.ProductType = type;
            product.ParentProductID = parentProductID;
            product.ExternalProductIdentifier = externalProductIdentifier;
            product.Created = DateTime.Parse(created);
            product.CreatedBy = createdBy;

            _context.Product.Add(product);
            _context.SaveChanges();
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
