﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
//using System.Web.Script.Serialization;
using Newtonsoft;
using System.IO;
using NEXT.API.Models;
using NEXT.API.Repositories;
using NEXT.API.Query;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NEXT.API
{
    [Route("api/product")]
    public class ProductController : Controller
    {

        private static JsonSerializer serializer = new JsonSerializer();
        private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings {ReferenceLoopHandling= ReferenceLoopHandling.Ignore };
        
        private IProductRepository productRepo;
        private IProductTypeRepository typeRepo;
        private IBrandRepository brandRepo;


        public ProductController( IProductRepository productRepo, IProductTypeRepository typeRepo, IBrandRepository brandRepo)
        {
            this.brandRepo = brandRepo;
            this.productRepo = productRepo;
            this.typeRepo = typeRepo;
        }

        // GET: api/category
        private static int defaultPage = 0;
        private static int defaultPageResults = 25;


        [HttpGet]
        public String Get([FromQuery][Bind("min_Created,max_Created,CreatedBy,ExternalProductIdentifier,min_LastModified,max_LastModified,LastModifiedBy,ParentProductID,ProductTypeID,SKU,orderBy,ascending")]ProductQuery query, [FromQuery]int page, [FromQuery]int results)
        {
            int total;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("data", productRepo.getProducts(query, page, results, out total));
            dictionary.Add("meta", total.ToString());

            return JsonConvert.SerializeObject(dictionary, serializerSettings);
        }

        // GET: api/product
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(productRepo.getProductByID(id), serializerSettings);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromForm][Bind("SKU,BrandID,Created,CreatedBy,ExternalProductIdentifier,LastModified,LastModifiedBy,ParentProductID,ProductTypeID", Prefix = "p")] Product product,
                         [FromForm][Bind("Name,ID", Prefix = "b")]Brand newBrand,
                         [FromForm][Bind("Name,ID", Prefix = "t")]ProductType newType)
        {
            if (ModelState.IsValid)
            {
                productRepo.insertProduct(product, newType, newBrand);
                productRepo.Save();
            }
            else {
                Response.StatusCode = 400;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromForm][Bind("SKU,BrandID,Created,CreatedBy,ExternalProductIdentifier,LastModified,LastModifiedBy,ParentProductID,ProductTypeID") ]Product product)
        {
            productRepo.updateProduct(product);
            productRepo.Save();
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
