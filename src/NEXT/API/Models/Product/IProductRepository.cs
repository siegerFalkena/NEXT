using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public interface IProductRepository: IDisposable
    {
        Product getProductByID(int productID);
        IEnumerable<Product> getProducts(ProductQuery query, int page, int results, out int total);
        void insertProduct(Product product, ProductType type, Brand brand);
        void insertProduct(Product product, Brand brand);
        void insertProduct(Product product, ProductType type);
        void insertProduct(Product product);
        void deleteProduct(int productID);
        void updateProduct(Product product);
        void Save();
    }
}
