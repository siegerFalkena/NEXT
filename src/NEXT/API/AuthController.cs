using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace API
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {   
        


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

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]string value)
        {
            await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal);
        }
        
        [HttpPut("{id}")]
        public String Put(int id, [FromBody]string value)
        {
            StringBuilder builder = new StringBuilder();
            try {
                
                foreach (String key in Request.Cookies.Keys) {
                    builder.Append(key);
                }

                } catch (Exception e) {
                builder.Append(e.ToString());
            }
            return builder.ToString();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
