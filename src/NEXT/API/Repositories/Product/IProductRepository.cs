using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Query;
using NEXT.API.Resource;

namespace NEXT.API.Repositories
{
    public interface IProductRepository: IDisposable
    {
        API.Resource.Product getProductByID(int productID);
        IEnumerable<API.Resource.Product> getProducts(ProductQuery query, out int total);
        void insertProduct(API.Resource.Product product, API.Resource.ProductType type, API.Resource.Brand brand);
        void insertProduct(API.Resource.Product product, API.Resource.Brand brand);
        void insertProduct(API.Resource.Product product, API.Resource.ProductType type);
        void insertProduct(API.Resource.Product product);
        void deleteProduct(int productID);
        void updateProduct(API.Resource.Product product);
        void Save();
    }
}
