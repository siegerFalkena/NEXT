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
using NEXT.API.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{
    [Route("api/product")]
    public class ProductController : Controller
    {

        private static List<string> names = new List<string> {"Papaya", "Banana", "Cherry", "Apple", "Pear" , "Orange", "Durian" , "Dragonfruit"
            , "Kiwi", "Qumquat", "Lime", "Lemon", "Jujube", "Olive", "Melon", "Tomato", "Coconut", "Apple",
"Apricot",
"Avocado",
"Banana",
"Bilberry",
"Blackberry",
"Blackcurrant",
"Blueberry",
"Boysenberry",
"Cantaloupe",
"Currant",
"Cherry",
"Cherimoya",
"Cloudberry",
"Coconut",
"Cranberry",
"Damson",
"Date",
"Dragonfruit",
"Durian",
"Elderberry",
"Feijoa",
"Fig",
"Goji berry",
"Gooseberry",
"Grape",
"Raisin",
"Grapefruit",
"Guava",
"Huckleberry",
"Jabuticaba",
"Jackfruit",
"Jambul",
"Jujube",
"Juniper berry",
"Kiwifruit",
"Kumquat",
"Lemon",
"Lime",
"Loquat",
"Lychee",
"Mango",
"Marionberry",
"Melon",
"Cantaloupe",
"Honeydew",
"Watermelon",
"Miracle fruit",
"Mulberry",
"Nectarine",
"Nance",
"Olive",
"Orange",
"Blood orange",
"Clementine",
"Mandarine",
"Tangerine",
"Papaya",
"Passionfruit",
"Peach",
"Pear",
"Persimmon",
"Physalis",
"Plantain",
"Plum/prune(dried plum)",
"Pineapple",
"Pomegranate",
"Pomelo",
"Purple mangosteen",
"Quince",
"Raspberry",
"Salmonberry",
"Rambutan",
"Redcurrant",
"Salal berry",
"Salak",
"Satsuma",
"Star fruit",
"Strawberry",
"Tamarillo",
"Tamarind",
"Ugli fruit"};


        JsonSerializer serializer = new JsonSerializer();
        private static ILogger LOG = LoggerFactory.
        private ApplicationContext _context;
        public ProductController(ApplicationContext context)
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
            IQueryable<Product> products = _context.products.Where<Product>(product => product.ID == id);
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
                product.price = double.Parse(price);
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
            Product product = _context.products.Where<Product>(pSelect => pSelect.ID == id).Single();
            product.name = name;
            product.description = description;
            product.price = double.Parse(price);
            _context.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
