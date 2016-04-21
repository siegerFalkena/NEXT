using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Routing.Template;

namespace NEXT
{
    public class AuthRouter : IRouter
    {
        private RouteBuilder routes; 
        public AuthRouter() {
            routes = new RouteBuilder();
        }

         
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            
         
            await context.HttpContext.Response.WriteAsync($"HEY!");

        }
            
    }
}
