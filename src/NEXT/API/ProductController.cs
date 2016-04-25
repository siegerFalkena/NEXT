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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace API
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {

        JsonSerializer serializer = new JsonSerializer();
        public class Product
        {
            public string name;
            public int ID;
            public double price;

            public Product(int id, string name, double price)
            {
                this.ID = id;
                this.name = name;
                this.price = price;
            }
        }

        public List<Product> productList()
        {
            List<Product> productList = new List<Product>();
            productList.Add(new Product(1, "dragonfruit", 9.999));
            productList.Add(new Product(1, "apple", 9.999));
            productList.Add(new Product(1, "pear", 9.999));
            productList.Add(new Product(1, "durian", 9.999));
            productList.Add(new Product(1, "cherry", 9.999));
            productList.Add(new Product(1, "papapaya", 9.999));
            productList.Add(new Product(1, "banana", 9.999));
            productList.Add(new Product(1, "granaatapple", 9.999));
            productList.Add(new Product(1, "limoen", 9.999));
            productList.Add(new Product(1, "citroen", 9.999));
            productList.Add(new Product(1, "meloen", 9.999));
            return productList;
        }

        private List<String> tempObjectStore = new List<String>();
        // GET: api/product
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> jsonlist = new List<string>();
            foreach (Product p in productList()) {
                jsonlist.Add(JsonConvert.SerializeObject(p));
            }
            return jsonlist;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
