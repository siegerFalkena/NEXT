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
    [Route("api/brand")]
    public class BrandController : Controller
    {
        IBrandRepository brandRepo;
        public BrandController(IBrandRepository brandRepo) {
            this.brandRepo = brandRepo;
        }

        // GET: api/values
        [HttpGet]
        public string Get( [FromQuery][Bind("brandNameContains,ascending,orderBy")] BrandQuery query, [FromQuery] int skipPage, [FromQuery] int results)
        {
            IEnumerable<Brand> brands = brandRepo.getBrands(query, skipPage, results);
            return JsonConvert.SerializeObject(brands);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(brandRepo.getBrandByID(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm]string name)
        {
            Brand brand = new Brand();
            brand.Name = name;
            brandRepo.insertBrand(brand);
            brandRepo.Save();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromQuery]string name)
        {
            Brand brand = new Brand();
            brand.ID = id;
            brand.Name = name;
            brandRepo.updateBrand(brand);
            brandRepo.Save();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            brandRepo.deleteBrand(id);
            brandRepo.Save();
        }
    }
}
