using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNet.Mvc;
using NEXT.API.Models;
using NEXT.API.Repositories;
using NEXT.API.Query;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/producttype")]
    public class ProductTypeController : Controller
    {
        IProductTypeRepository productTypeRepo;
        public ProductTypeController(IProductTypeRepository productTypeRepo)
        {
            this.productTypeRepo = productTypeRepo;
        }

        // GET: api/values
        [HttpGet]
        public string Get([FromQuery] int results, [FromQuery] int skipPage)
        {
            ProductTypeQuery query = new ProductTypeQuery();
            IEnumerable<ProductType> ProductTypes = productTypeRepo.getProductTypes(query, skipPage, results);
            return JsonConvert.SerializeObject(ProductTypes);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(productTypeRepo.getProductTypeByID(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm]string name)
        {
            ProductType productType = new ProductType();
            productType.Name = name;
            productTypeRepo.insertProductType(productType);
            productTypeRepo.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productTypeRepo.deleteProductType(id);
            productTypeRepo.Save();
        }
    }
}
