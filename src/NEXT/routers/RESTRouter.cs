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
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            return null;
        }

        public async Task RouteAsync(RouteContext context)
        {
            /*{
            ID: 1,
            name: 'appel',
            price: '9.99'
        }, {
            ID: 2,
            name: 'peer',
            price: '8.99'
        }, {
            ID: 3,
            name: 'banaan',
            price: '7.99'
        }, {
            ID: 4,
            name: 'granaatappel',
            price: '6.99'
        }, {
            ID: 5,
            name: 'kiwi',
            price: '6.99'
        }, {
            ID: 6,
            name: 'papaya',
            price: '6.99'
        }, {
            ID: 8,
            name: 'lemon',
            price: '6.99'
        }, {
            ID: 9,
            name: 'melon',
            price: '6.99'
        }, {
            ID: 10,
            name: 'blueberry',
            price: '6.99'
        }, {
            ID: 11,
            name: 'dragonfruit',
            price: '6.99'
        }, {
            ID: 12,
            name: 'coconut',
            price: '6.99'
        }, {
            ID: 13,
            name: 'lime',
            price: '6.99'
        }, {
            ID: 14,
            name: 'peach',
            price: '6.99'
        }, {
            ID: 15,
            name: 'tomato',
            price: '6.99'
        }*/
            
            await context.HttpContext.Response.WriteAsync($"HEY!");
            context.IsHandled = true;
        }
            
    }
}
