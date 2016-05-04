using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

using NEXT.API.Models;
using NEXT.API.Repositories;
using NEXT.API.Query;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{

    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> logger;
        private ApplicationContext ctx;
        private IProductRepository productRepo;

        public CategoryController(ApplicationContext ctx, ILogger<CategoryController> logger, IProductRepository productRepo)
        {
            this.logger = logger;
            this.ctx = ctx;
            this.productRepo = productRepo;
        }


        // GET: api/category
        private static int defaultPage = 0;
        private static int defaultPageResults = 25;

        [HttpGet]
        public String Get([FromQuery]int? page, [FromQuery]int? results, [FromQuery]string name, [FromQuery]double? filter_minPrice, [FromQuery]double? filter_maxPrice)
        {
            page = page == null ? page : defaultPage;
            results = results == null ? results : defaultPageResults;
            
            ProductQuery query = new ProductQuery();

            IEnumerable<Product> products = productRepo.getProducts(query, page ?? default(int), results ?? default(int));

            return JsonConvert.SerializeObject(products);
        }

        // GET api/category/5
        [HttpGet("{categoryID}")]
        public string Get(int categoryID)
        {
            var category = ctx.categories.Where<Category>(Category => Category.categoryID == categoryID);
            return JsonConvert.SerializeObject(category);
        }

        // POST api/category
        [HttpPost]
        public void Post([FromBody]string description)
        {
            Category cat = new Category();
            try
            {
                cat.description = description;
                cat.name = "name";
                ctx.categories.Add(cat);
                ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                cat.description = e.Message.ToString();
                cat.name = e.Message.ToString();
                ctx.categories.Add(cat);
                ctx.SaveChanges();
            }
        }



        // PUT api/values/5
        [HttpPut("{categoryID}")]
        public void Put(int categoryID, [FromForm]string description, [FromForm] string name)
        {
            Category cat = ctx.categories.Where<Category>(category => category.categoryID == categoryID).Single();
            cat.description = description;
            cat.name = name;
            ctx.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
