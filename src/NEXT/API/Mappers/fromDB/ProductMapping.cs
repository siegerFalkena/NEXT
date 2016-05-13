using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using System.Reflection;
using NEXT.DB.Models;
using NEXT.API.Resource;

namespace NEXT.API.Mappers
{
    public class ProductMapping
    {
        private MapperConfiguration _config;
        private IMapper mapper;
        
        public ProductMapping() {
            _config = new MapperConfiguration(cfg => { cfg.CreateMap<DB.Models.Product, API.Resource.Product>(); });
            mapper = _config.CreateMapper();
        }

        public API.Resource.Product map(DB.Models.Product dbProduct) {
            return mapper.Map<DB.Models.Product, API.Resource.Product>(dbProduct);
        }
    }
}
