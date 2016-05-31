using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NEXT.API.Query;
using NEXT.API.Resource;
using NEXT.API.Repositories;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/vendor")]
    public class VendorController : Controller
    {
        private IVendorRepository _vendorRepo;
        public VendorController(IVendorRepository vendorRepo) {
            this._vendorRepo = vendorRepo;
        }

        // GET: api/values
        [HttpGet]
        public JsonResult Get([Bind][FromQuery] VendorQuery query)
        {
            return Json(_vendorRepo.query(query));
        }
        
    }
}
