using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;

namespace NEXT
{
    public class appRouter : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            //Do bs REST stuff
            await context.HttpContext.Response.WriteAsync($"Hi !");
            context.IsHandled = true;

        }
            
    }
}
