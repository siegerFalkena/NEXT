using System;
using System.Collections.Generic;
using System.Reflection;
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
using NEXT.API.Resource;
using NEXT.API.Repositories;
using AutoMapper;
using AutoMapper.Mappers;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        JsonSerializer serializer = new JsonSerializer();
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings() {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore

        };
        IUserRepository userRepo;

        public UserController(IUserRepository repository)
        {
            this.userRepo = repository;
        }


        private List<string> tempObjectStore = new List<string>();
        // GET: api/user
        [HttpGet]
        public String Get([FromQuery]string containsFirstName, [FromQuery] string containsLastName, [FromQuery] string containsUsername, [FromQuery] int results, [FromQuery] int page)
        {
            UserQuery query = new UserQuery();
            query.userName = containsUsername == null ? containsUsername : null;
            query.firstNameContains = containsFirstName == null ? containsFirstName : null;
            query.lastNameContains = containsLastName == null ? containsLastName : null;
            int queryResults = results == 0 ? 25: results;
            return JsonConvert.SerializeObject(userRepo.userQuery(query), serializerSettings) ;
        }

        // GET: api/user
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(userRepo.getUserByID(id));
        }


        // POST api/user
        //TODO need authorization
        [HttpPost]
        public void Post([FromForm]string firstName, [FromForm]string lastName, [FromForm]string userName, [FromForm]string password, [FromForm] string email, [FromForm]int companyID)
        {
          
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromForm] string name, [FromForm] string description, [FromForm] string price)
        {
            
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userRepo.deleteUser(id);
            userRepo.save();
        }
    }
}
