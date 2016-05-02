using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using NEXT.API.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        public static Random r = new Random();
        public static List<Category> categories = generateCategories(20);

        public static List<Category> generateCategories(int number)
        {
            string[] typicalKeywords = { "sweet", "sour", "tangy", "green", "red", "hard", "soft", "dangerous" };
            List<Category> list = new List<Category>(number);
            for (int i = 0; i < number; i++)
            {
                Category cat = new Category(i);
                for (int j = 0; j < r.Next(typicalKeywords.Length); j++) {
                    cat.keywords.Add(typicalKeywords[r.Next(typicalKeywords.Length)]);
                }
                list.Add(cat);
            }
            return list;
        }

        // GET: api/category
        [HttpGet]
        public String Get()
        {
            return JsonConvert.SerializeObject(categories);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            try {
                return JsonConvert.SerializeObject(categories[id]);
            }
            catch (Exception e) {
                return JsonConvert.SerializeObject(e);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
