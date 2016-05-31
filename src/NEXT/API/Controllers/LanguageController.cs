using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NEXT.API.Repositories;
using NEXT.API.Resource;
using NEXT.API.Query;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{

    [Route("api/language")]
    public class LanguageController : Controller
    {
        private ILanguageRepository _langRepo;
        public LanguageController(ILanguageRepository langRepo) {
            this._langRepo = langRepo;
        }
        // GET: api/values
        [HttpGet]
        public JsonResult Get([Bind][FromQuery] LanguageQuery query)
        {
            return Json(_langRepo.query(query));
        }
    }
}
