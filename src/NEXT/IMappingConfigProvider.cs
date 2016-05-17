using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace NEXT
{
    public interface IMappingConfigProvider
    {
        //handle with care
        MapperConfiguration getConfig();
    }
}
