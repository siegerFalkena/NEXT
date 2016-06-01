using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NEXT.API.Query;
using NEXT.API.Repositories;
using NEXT.API.Resource;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API.Controllers
{

    [Route("api/attribute")]
    public class AttributeController : Controller
    {

        IAttributeRepository _attributeRepo;
        IAttributeTypeRepository _attributeTypeRepo;

        public AttributeController(IAttributeTypeRepository typeRepo, IAttributeRepository attributeRepo)
        {
            _attributeRepo = attributeRepo;
            _attributeTypeRepo = typeRepo;
        }

        // GET: api/values
        [HttpGet("type")]
        public JsonResult Get([FromQuery][Bind] AttributeTypeQuery typeQuery)
        {
            ICollection<AttributeType> attributeTypes = _attributeTypeRepo.query(typeQuery);
            return Json(attributeTypes);
        }

        // GET: api/values
        [HttpGet()]
        public JsonResult Get([FromQuery][Bind] AttributeQuery attQuery)
        {
            ICollection<Resource.Attribute> attributes = _attributeRepo.query(attQuery);
            return Json(attributes);
        }

        [HttpGet("{AttributeID}")]
        public JsonResult getByID(int AttributeID) {
            return Json(_attributeRepo.getByID(AttributeID));
        }
    }
}
