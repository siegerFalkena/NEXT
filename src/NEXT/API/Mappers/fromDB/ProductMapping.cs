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

        public ProductMapping()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DB.Models.ProductType, API.Resource.ProductType>();
                cfg.CreateMap<DB.Models.Product, API.Resource.Product>()
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.ProductType))
                .ForMember(dest => dest.brandName, opt => opt.MapFrom(src => src.Brand.Name));
                cfg.CreateMap<DB.Models.Brand, API.Resource.Brand>()
                .ForMember(dest => dest.brandID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            });
            mapper = _config.CreateMapper();
        }

        public API.Resource.Product map(DB.Models.Product dbProduct)
        {
            return mapper.Map<DB.Models.Product, API.Resource.Product>(dbProduct);
        }
    }
}
