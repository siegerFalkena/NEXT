using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNet.Mvc;
using NEXT.API.Models;
using NEXT.API.Repositories;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/company")]
    public class CompanyController : Controller
    {
        private ICompanyRepository repository;
        public CompanyController(ICompanyRepository repository) {
            this.repository = repository;
        }

        // GET: api/company
        [HttpGet]
        public string Get(int results, int skipPage)
        {
            CompanyQuery query = new CompanyQuery();
            string companyLsit = JsonConvert.SerializeObject(repository.companyQuery(query, results, skipPage));
            return companyLsit; 
        }

        // GET api/company/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Company comp = repository.getCompanyByID(id);
            return JsonConvert.SerializeObject(comp);
        }

        // POST api/company
        [HttpPost]
        public void Post([FromForm]string isActive, [FromForm] string name)
        {
            Company comp = new Company();
            bool parseActive;
            bool.TryParse(isActive, out parseActive);
            comp.IsActive = parseActive;
            comp.Name = name;
            repository.createCompany(comp);
            repository.save();
        }

        // PUT api/company/5
        [HttpPut("{id}")]
        public void Put(int id)
        {
            throw new NotImplementedException();
        }

        // DELETE api/company/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
