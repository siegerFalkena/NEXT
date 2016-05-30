using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Repositories;
using NEXT.API.Query;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/producttype")]
    public class ProductTypeController : Controller
    {
        private IProductTypeRepository typeRepo;
        public ProductTypeController(IProductTypeRepository typeRepo)
        {
            this.typeRepo = typeRepo;
        }

        // GET: api/values
        [HttpGet("query", Name = "typeQuery")]
        public JsonResult Query([FromQuery][Bind]ProductTypeQuery query)
        {
            return Json(typeRepo.query(query));
        }

    }
}
