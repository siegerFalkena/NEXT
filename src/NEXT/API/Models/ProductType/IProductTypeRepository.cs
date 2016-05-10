using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public interface IProductTypeRepository: IDisposable
    {
        ProductType getProductTypeByID(int ProductTypeID);
        IEnumerable<ProductType> getProductTypes(ProductTypeQuery query, int page, int results);
        void insertProductType(ProductType ProductType);
        void deleteProductType(int ProductTypeID);
        void updateProductType(ProductType ProductType);
        void Save();
    }
}
