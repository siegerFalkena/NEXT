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
        IEnumerable<API.Resource.Product> getProducts(ProductQuery query, int page, int results, out int total);
        void insertProduct(DB.Models.Product product, DB.Models.ProductType type, DB.Models.Brand brand);
        void insertProduct(DB.Models.Product product, DB.Models.Brand brand);
        void insertProduct(DB.Models.Product product, DB.Models.ProductType type);
        void insertProduct(DB.Models.Product product);
        void deleteProduct(int productID);
        void updateProduct(DB.Models.Product product);
        void Save();
    }
}
