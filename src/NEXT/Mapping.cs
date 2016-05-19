using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using NEXT.DB.Models;
using NEXT.API.Resource;

namespace NEXT
{

    public class Mapping : IMappingConfigProvider
    {
        private static MapperConfiguration config;
        MapperConfiguration IMappingConfigProvider.getConfig()
        {

            if (config == null)
            {
                config = new MapperConfiguration(cfg =>
               {
                   //toResource
                   cfg.CreateMap<DB.Models.ProductType, API.Resource.ProductType>();

                   cfg.CreateMap<DB.Models.ProductAttributeValue, API.Resource.Attribute>()
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                   .ForMember(dest => dest.AttributeType, opt => opt.MapFrom(src => src.Attribute.AttributeType.Name));

                   cfg.CreateMap<DB.Models.Vendor, API.Resource.Vendor>();
                   cfg.CreateMap<DB.Models.Product, API.Resource.Product>()
                   .ForMember(dest => dest.productID, opt => opt.MapFrom(src => src.ID))
                   .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.Brand))
                   .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                   .ForMember(dest => dest.relatedProduct, opt => opt.MapFrom(src => src.RelatedProduct))
                   .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified))
                   .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
                   .ForMember(dest => dest.attributeValues, opt => opt.MapFrom(src => src.ProductAttributeValue))
                   .ForMember(dest => dest.attributeValues, opt => opt.MapFrom(src => src.ProductAttributeValue))
                   .ForMember(dest => dest.attributeOptions, opt => opt.MapFrom(src => src.ProductAttributeOption))
                   .ForMember(dest => dest.relatedProductNavigation, opt => opt.MapFrom(src => src.RelatedProductNavigation));

                   cfg.CreateMap<DB.Models.Brand, API.Resource.Brand>()
                   .ForMember(dest => dest.brandID, opt => opt.MapFrom(src => src.ID))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                   cfg.CreateMap<DB.Models.Company, API.Resource.Company>();
                   cfg.CreateMap<DB.Models.User, API.Resource.User>();



                   cfg.CreateMap<DB.Models.ProductAttributeOption, API.Resource.Attribute>();


                   //fromResource
                   cfg.CreateMap<API.Resource.Brand, DB.Models.Brand>();

                   cfg.CreateMap<API.Resource.ProductType, DB.Models.ProductType>();

                   cfg.CreateMap<API.Resource.Product, DB.Models.Product>()
                   .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.brand))
                   .ForMember(dest => dest.ChannelProduct, opt => opt.MapFrom(src => src.channel))
                   .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                   .ForMember(dest => dest.RelatedProduct, opt => opt.MapFrom(src => src.relatedProduct))
                   .ForMember(dest => dest.RelatedProductNavigation, opt => opt.MapFrom(src => src.relatedProductNavigation));
               });
            }
            return config;
        }

        private class customResolver : ValueResolver<DB.Models.VendorProduct, DB.Models.Vendor>
        {
            protected override DB.Models.Vendor ResolveCore(VendorProduct source)
            {
                return source.Vendor;
            }
        }
    }
}
