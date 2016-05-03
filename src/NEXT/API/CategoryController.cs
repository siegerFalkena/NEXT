using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

using NEXT.API.Model;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{

    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> logger;
        private ApplicationContext ctx;

        public CategoryController(ApplicationContext ctx,  ILogger<CategoryController> logger)
        {
            this.logger = logger;
            this.ctx = ctx;
        }


        // GET: api/category
        [HttpGet]
        public String Get()
        {
            return JsonConvert.SerializeObject(ctx.products.ToList());
        }

        // GET api/category/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var category= ctx.categories.Where<Category>(Category => Category.id == id);
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
