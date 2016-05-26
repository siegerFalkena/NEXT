using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NEXT.API.Repositories;
using NEXT.API.Query;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{
    [Route("api/brand")]
    public class BrandController : Controller
    {

        IBrandRepository _brandRepo;
        public BrandController(IBrandRepository brandRepo) {
            _brandRepo = brandRepo;
        }

        // GET: api/values
        [HttpGet("query", Name = "brandQuery")]
        public JsonResult Query([FromQuery][Bind]BrandQuery query)
        {
            
            return Json(_brandRepo.query(query));
        }
        
    }
}
