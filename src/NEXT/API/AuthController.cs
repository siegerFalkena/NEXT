using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System.Text;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace API
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private static CookieOptions defaultCookieOptions;
        public AuthController() {
            defaultCookieOptions = new CookieOptions {
                Domain =  "localhost"
            };
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/auth
        [HttpPost]
        public string Post([FromHeader]string username, [FromHeader]string password)
        {
            StringBuilder bulder = new StringBuilder();
            foreach (string key in Request.Headers.Keys) {
                bulder.Append(key);
            }
            Response.Cookies.Append("authToken", "someValue", defaultCookieOptions);
            Response.Cookies.Append("user", "someValue", defaultCookieOptions);
            Response.Cookies.Append("role", "someValue", defaultCookieOptions);
            return bulder.ToString();
        }
        
        [HttpPut("{id}")]
        public string Put(int id, [FromBody]string value)
        {
            return "";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
