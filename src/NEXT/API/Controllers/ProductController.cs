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
using NEXT.DB.Models;
using NEXT.API.Repositories;
using NEXT.API.Query;
using NEXT.API.Serializer;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{
    [Route("api/product")]
    public class ProductController : Controller
    {

        private static JsonSerializer serializer = new JsonSerializer();
        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings {ReferenceLoopHandling= ReferenceLoopHandling.Ignore };
        
        private IProductRepository productRepo;
        private IProductTypeRepository typeRepo;


        public ProductController( IProductRepository productRepo, IProductTypeRepository typeRepo)
        {
            this.productRepo = productRepo;
            this.typeRepo = typeRepo;
        }

        // GET: api/category
        private static int defaultPage = 0;
        private static int defaultPageResults = 25;


        [HttpGet]
        public String Get([FromQuery][Bind]ProductQuery query, [FromQuery]int page, [FromQuery]int results)
        {
            int total;
            string data = "";
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            IEnumerable<API.Resource.Product> products =  productRepo.getProducts(query, page, results, out total);
            using (var strWriter = new StringWriter()) {
                using (var jsonWriter = new CustomJsonTextWriter(strWriter)) {
                    Func<bool> include = () => jsonWriter.CurrentDepth <= 2;
                    var resolver = new CustomContractResolver(include);
                    var serializer = new JsonSerializer { ContractResolver = resolver,  ReferenceLoopHandling = ReferenceLoopHandling.Serialize};
                    serializer.Serialize(jsonWriter, products);
                }
                data = strWriter.ToString();
                dictionary.Add("data", data);
            }
            dictionary.Add("meta", total.ToString());
            
            return data;
        }

        // GET: api/product
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(productRepo.getProductByID(id), serializerSettings);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromForm][Bind] Product product,
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
        public void Put([FromForm][Bind("SKU,BrandID,Created,CreatedBy,ExternalProductIdentifier,LastModified,LastModifiedBy,ParentProductID,ProductTypeID") ]Product product)
        {
            productRepo.updateProduct(product);
            productRepo.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productRepo.deleteProduct(id);
            productRepo.Save();
        }
    }
}
