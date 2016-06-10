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
        [HttpPost()]
        public JsonResult auth([FromHeader]string username, [FromHeader] string password)
        {
            Response.Cookies.Append("authToken", "true", new Microsoft.AspNet.Http.CookieOptions { Expires = DateTime.Now.AddDays(1)});
            Response.Cookies.Append("user", username, new Microsoft.AspNet.Http.CookieOptions { Expires = DateTime.Now.AddDays(1) });
            Response.Cookies.Append("role", "userRole", new Microsoft.AspNet.Http.CookieOptions { Expires = DateTime.Now.AddDays(1) });
            Redirect("#/product/overview");
            return Json(new KeyValuePair<string, string>(username, password));
        }

    }
}
