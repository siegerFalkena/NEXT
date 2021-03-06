﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;
//using System.Web.Script.Serialization;
using Newtonsoft;
using System.IO;
using NEXT.API.Resource;
using NEXT.API.Repositories;
using NEXT.API.Query;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{
    [Route("api/product")]
    public class ProductController : Controller
    {

        private static JsonSerializer serializer = new JsonSerializer();
        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        private IProductRepository productRepo;
        private IMappingConfigProvider mapConfig;

        public ProductController(IProductRepository productRepo, IMappingConfigProvider mapConfig)
        {
            this.productRepo = productRepo;
            this.mapConfig = mapConfig;
        }

        private JsonResult getFunctions()
        {
            MethodInfo[] methodInfos = Type.GetType(typeof(ProductController).ToString()).GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            Dictionary<string, object> toBeSerialized = new Dictionary<string, object>();
            ParameterInfo[] tempPInfo = methodInfos[1].GetParameters();
            Dictionary<string, Dictionary<string, string>> methodDict = new Dictionary<string, Dictionary<string, string>>();
            foreach (MethodInfo mInfo in methodInfos)
            {
                ParameterInfo[] pInfos = mInfo.GetParameters();
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                foreach (ParameterInfo pInfo in pInfos)
                {
                    parameters.Add(pInfo.Name, pInfo.ParameterType.Name);
                }
                methodDict.Add(mInfo.Name, parameters);
            };
            return Json(
                methodDict
                );
        }

        [HttpGet("{productID}/children")]
        public JsonResult getChildren(int productID, [FromQuery]int results, [FromQuery]int page)
        {
            return Json(productRepo.getChildren(productID, results, page));
        }


        [HttpGet]
        public JsonResult Root()
        {
            return getFunctions();
        }

        // GET: api/product/query
        [HttpGet("query", Name = "query")]
        public JsonResult List([FromQuery][Bind]ProductQuery query)
        {
            int total;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            IEnumerable<API.Resource.Product> products = productRepo.getProducts(query, out total).ToArray();
            dictionary.Add("meta", total.ToString());
            dictionary.Add("data", products);
            dictionary.Add("results", total);
            return Json(dictionary, serializerSettings);
        }

        [HttpGet("{id}/attributes")]
        public JsonResult productAttribute(int id)
        {
            return Json(productRepo.getAttributes(id));
        }

        [HttpPost("{id}/attributes")]
        public JsonResult updateOrSaveAttribute([FromBody][Bind] API.Resource.ProductAttribute attribute)
        {
            return Json(productRepo.createOrUpdateAttribute(attribute));
        }

        [HttpGet("{id}/brand")]
        public JsonResult getBrand(int id)
        {
            return Json(productRepo.getBrand(id));
        }

        [HttpPost("{id}/brand")]
        public JsonResult setBrand([FromBody][Bind]Brand brand, int id)
        {
            return Json(productRepo.setBrand(id, brand));
        }


        [HttpGet("{id}/type")]
        public JsonResult productType(int id)
        {
            return Json(productRepo.getType(id));
        }

        [HttpPost("{id}/type")]
        public JsonResult setType([FromBody][Bind] ProductType type, int id)
        {
            return Json(productRepo.setType(id, type));
        }


        // GET: api/product
        [HttpGet("{id}")]
        public JsonResult GetByID(int id)
        {
            Product tcudorp = productRepo.getProductByID(id);
            if (tcudorp != null)
            {
                return Json(tcudorp, serializerSettings);
            }
            else
            {
                base.HttpContext.Response.StatusCode = 404;
                return Json(tcudorp);
            }
        }

        // GET: api/product
        [HttpGet("{id}/channels")]
        public JsonResult getChannelsByID(int id)
        {
            return Json(productRepo.getChannels(id), serializerSettings);
        }

        // GET: api/product
        [HttpGet("{id}/vendors")]
        public JsonResult getVendorsByID(int id)
        {
            return Json(productRepo.getVendors(id), serializerSettings);
        }


        // POST api/values
        //TODO make some sensible return structure
        [HttpPost]
        public JsonResult Post([FromBody][Bind] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.productID == 0)
                {
                    Product savedProduct = productRepo.createProduct(product);
                    return Json(savedProduct);
                }
                else
                {
                    productRepo.updateProduct(product);
                    return Json("success");
                };
            }
            else
            {
                base.HttpContext.Response.StatusCode = 422;
                return Json("missing parameters");
            }
        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody][Bind]Product product)
        {

        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productRepo.deleteProduct(id);
            productRepo.Save();
        }
    }
}
