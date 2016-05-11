using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using NEXT.API.Repositories;
using NEXT.API.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/authenticate")]
    public class AuthenticationController : Controller
    {

        static ClaimsPrincipal principal = ClaimsPrincipal.Current;
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        IUserRepository userRepo;
        public AuthenticationController(IUserRepository userRepo, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task authUser([FromForm] string username, [FromForm] string password, [FromForm]bool isPersistent)
        {

            User user = new Models.User();
            user.Username = username;
            user.Password = password;
            await signInManager.SignInAsync(user, isPersistent);
        }


        [HttpPost("logout")]
        public async Task logoutUser()
        {
            await HttpContext.Authentication.SignOutAsync("NEXTAuthenticationScheme");
        }
    }
}
