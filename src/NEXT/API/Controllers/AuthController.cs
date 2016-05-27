using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        // GET: api/values
        [HttpPost("/auth")]
        public void auth([FromHeader]string username, [FromHeader] string password)
        {
            
        }

    }
}
