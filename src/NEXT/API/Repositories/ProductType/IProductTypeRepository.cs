﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.DB.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public interface IProductTypeRepository : IDisposable
    {
        ICollection<Resource.ProductType> query(ProductTypeQuery query);
        
    }
}
