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

        public ProductController(NEXTContext context, IProductRepository productRepo)
        {
            this._context = context;
            this.productRepo = productRepo;
        }

        // GET: api/category
        private static int defaultPage = 0;
        private static int defaultPageResults = 25;


        [HttpGet]
        public String Get([FromQuery]string page, [FromQuery]string results, [FromQuery]string name, [FromQuery]string fpricemin, [FromQuery]string fpricemax)
        {
            ProductQuery query = new ProductQuery();
            query.minPrice = fpricemin;
            query.maxPrice = fpricemax;
            query.nameContains  = name;

            int queryPage;
            bool parsedPage = int.TryParse(page, out queryPage);
            int queryResults;
            bool parsedResults = int.TryParse(results, out queryResults);

            IEnumerable<Product> products = productRepo.getProducts(query,
                parsedPage ? queryPage : defaultPage,
                parsedResults ? queryResults : defaultPageResults);

            return JsonConvert.SerializeObject(products);
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
    public void Post([FromForm]string name, [FromForm]string description, [FromForm]string price, [FromForm]string body)
    {
        Product product = new Product();
        try
        {
            _context.Product.Add(product);
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
