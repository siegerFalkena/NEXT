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
        Product getProductByID(int productID);
        IEnumerable<Product> getProducts(ProductQuery query, out int total);
        void insertProduct(Product product, ProductType type, Brand brand);
        void insertProduct(Product product, Brand brand);
        void insertProduct(Product product, ProductType type);
        void insertProduct(Product product);
        API.Resource.Product createProduct(API.Resource.Product newProduct);
        void deleteProduct(int productID);
        void updateProduct(Product product);
        ICollection<API.Resource.ProductAttribute> getAttributes(int id);
        ICollection<Product> getRelatedProducts(int id, int results, int page);
        ICollection<Product> getRelatedNavigationProducts(int id, int results, int page);
        ICollection<Product> getChildren(int id, int results, int page);
        Product getParent(int id);
        Brand getBrand(int productID);
        void removeVendor(int productID, int vendorID);
        int setBrand(int productId, Brand brand);
        ProductType getType(int productID);
        int setType(int productID, ProductType type);
        ICollection<Vendor> getVendors(int productID);
        ICollection<Channel> getChannels(int productID);

        void Save();
    }
}
