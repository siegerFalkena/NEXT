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

                   cfg.CreateMap<DB.Models.ProductAttributeValue, API.Resource.ProductAttribute>()
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                   .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                   .ForMember(dest => dest.LanguageID, opt => opt.MapFrom(src => src.LanguageID))
                   .ForMember(dest => dest.AttributeID, opt => opt.MapFrom(src => src.AttributeID))
                   .ForMember(dest => dest.VendorID, opt => opt.MapFrom(src => src.VendorID));


                   cfg.CreateMap<DB.Models.Vendor, API.Resource.Vendor>()
                   .ForMember(dest => dest.VendorID, opt => opt.MapFrom(src => src.ID));

                   cfg.CreateMap<DB.Models.Language, API.Resource.Language>()
                   .ForMember(dest => dest.LanguageID , opt => opt.MapFrom(src => src.ID));

                   cfg.CreateMap<DB.Models.Attribute, API.Resource.Attribute>()
                   .ForMember(dest => dest.attributeID, opt => opt.MapFrom( src => src.ID))
                   .ForMember(dest => dest.attributeTypeName, opt => opt.MapFrom(src => src.AttributeType.Name));

                   //toResource CHANNEL
                   cfg.CreateMap<DB.Models.Channel, API.Resource.Channel>()
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                   cfg.CreateMap<DB.Models.Product, API.Resource.Product>()
                   .ForMember(dest => dest.productID, opt => opt.MapFrom(src => src.ID))
                   .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.Brand))
                   .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                   .ForMember(dest => dest.relatedProduct, opt => opt.MapFrom(src => src.RelatedProduct))
                   .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified))
                   .ForMember(dest => dest.ParentProductID, opt => opt.MapFrom(src => src.ParentProductID))
                   .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy))
                   .ForMember(dest => dest.attributeValues, opt => opt.MapFrom(src => src.ProductAttributeValue))
                   .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
                   .ForMember(dest => dest.ProductTypeID, opt => opt.MapFrom(src => src.ProductTypeID))
                   .ForMember(dest => dest.relatedProductNavigation, opt => opt.MapFrom(src => src.RelatedProductNavigation));

                   cfg.CreateMap<DB.Models.Brand, API.Resource.Brand>()
                   .ForMember(dest => dest.brandID, opt => opt.MapFrom(src => src.ID))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                   cfg.CreateMap<DB.Models.Company, API.Resource.Company>();

                   cfg.CreateMap<DB.Models.User, API.Resource.User>();

                   cfg.CreateMap<DB.Models.AttributeType, API.Resource.AttributeType>();

                   //fromResource
                   cfg.CreateMap<API.Resource.Brand, DB.Models.Brand>();

                   cfg.CreateMap<API.Resource.ProductAttribute, DB.Models.ProductAttributeValue>()
                   .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

                   cfg.CreateMap<API.Resource.ProductType, DB.Models.ProductType>();

                   cfg.CreateMap<API.Resource.Vendor, DB.Models.Vendor>();
                   cfg.CreateMap<API.Resource.Channel, DB.Models.Channel>();

                   cfg.CreateMap<API.Resource.Product, DB.Models.Product>()
                   .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.productID))
                   .ForMember(dest => dest.BrandID, opt => opt.MapFrom(src => src.BrandID))
                   .ForMember(dest => dest.ProductAttributeValue, opt => opt.MapFrom(src => src.attributeValues))
                   .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.brand))
                   .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
                   .ForMember(dest => dest.ParentProductID, opt => opt.MapFrom(src => src.ParentProductID))
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
