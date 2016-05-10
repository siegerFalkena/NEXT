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
        private IUserRepository userRepo;
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore

        };
        public CompanyController(ICompanyRepository repository, IUserRepository userRepo) {
            this.repository = repository;
            this.userRepo = userRepo;
        }

        // GET: api/company
        [HttpGet]
        public string Get([FromQuery]int results, [FromQuery]int skipPage)
        {
            CompanyQuery query = new CompanyQuery();
            string companyLsit = JsonConvert.SerializeObject(repository.companyQuery(query, results, skipPage));
            return JsonConvert.SerializeObject(companyLsit, serializerSettings);
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

        [HttpGet("{companyID}/user")]
        public string getUsersInCompany(int companyID) {
            Company company = repository.getCompanyByID(companyID);
            return JsonConvert.SerializeObject(company.User, serializerSettings);
        }

        [HttpGet("{companyID}/user")]
        public string addUserToCompany(int companyID, [FromForm] int userID)
        {
            Company company = repository.getCompanyByID(companyID);
            User user = userRepo.getUserByID(userID);
            repository.addUserToCompany(companyID, userID);
            repository.save();
            return JsonConvert.SerializeObject(company.User, serializerSettings);
        }

        // DELETE api/company/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repository.deleteCompany(id);
        }
    }
}
