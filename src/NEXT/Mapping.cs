using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;

namespace NEXT
{

    public class Mapping : IServiceProvider
    {
       
        public object GetService(Type serviceType)
        {
            return new MapperConfiguration(fun => fun.CreateMap<DB.Models.Product, API.Resource.Product>());
        }
    }
}
