using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using Microsoft.AspNet.Mvc;
using NEXT.API.Models;
using NEXT.API.Query;
using NEXT.API.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        private IBrandRepository brandRepo;
        public TestController(IBrandRepository brandRepo) {
            this.brandRepo = brandRepo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromForm]int value)
        {
            Random r = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < value; i++) {
                Brand newBrand = new Brand();
                byte[] randomString = new byte[24];
                r.NextBytes(randomString);
                newBrand.Name = System.Text.Encoding.Unicode.GetString(randomString);
                brandRepo.insertBrand(newBrand);
            };
            brandRepo.Save();
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
