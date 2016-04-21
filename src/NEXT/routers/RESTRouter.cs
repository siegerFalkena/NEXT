using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace NEXT
{
    public class RESTRouter : IRouter
    {
        private RouteBuilder routes;

        public RESTRouter() {
           
        }


        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            await context.HttpContext.Response.WriteAsync($"Hi !");
            
            context.IsHandled = true;

        }
            
    }
}
