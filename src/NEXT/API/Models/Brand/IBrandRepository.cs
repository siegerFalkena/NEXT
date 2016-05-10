using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public interface IBrandRepository: IDisposable
    {
        Brand getBrandByID(int BrandID);
        IEnumerable<Brand> getBrands(BrandQuery query, int page, int results);
        void insertBrand(Brand Brand);
        void deleteBrand(int BrandID);
        void updateBrand(Brand Brand);
        void Save();
    }
}
