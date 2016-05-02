using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private static Random random = new Random();
        private static List<Product> randomProductList(int number)
        {
            List<Product> products = new List<Product>();
            List<Category> categories = new List<Category>();
            for (int i = 0; i < number; i++)
            {
                string name = names[random.Next(names.Count)];
                int[] categoryKeys = new int[random.Next(CategoryController.categories.Count)];
                for (int j = 0; j < categoryKeys.Length; j++) {
                    categoryKeys[j] = CategoryController.categories[random.Next(CategoryController.categories.Count)].id;
                }
                Product product = new Product(i, name, random.NextDouble() * 20, categoryKeys);
                products.Add(product);
            }
            return products;
        }

        private List<Product> productList = randomProductList(200);


        JsonSerializer serializer = new JsonSerializer();
       

        private List<string> tempObjectStore = new List<string>();
        // GET: api/product
        [HttpGet]
        public String Get()
        {
            int page = 0;
            int results = 25;
            try
            {
                var query = Request.Query;
                if (query.Keys.Contains("page"))
                {
                    page = int.Parse(query["page"]);
                }
                if (query.Keys.Contains("results"))
                {
                    page = int.Parse(query["results"]);
                }
                List<Product> templist = productList.GetRange(page*results, results);
                return JsonConvert.SerializeObject(templist);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
        }

        // GET: api/product
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Product temp = null;
            try
            {
                temp = productList[id];
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(e);
            }
            return JsonConvert.SerializeObject(temp);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            tempObjectStore.Add(value);
            foreach (String val in tempObjectStore)
            {
                Debug.WriteLine(val);
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
