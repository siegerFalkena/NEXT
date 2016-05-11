using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NEXTContext _context;

        public ProductRepository(NEXTContext context) {
            this._context = context;
        }

        public void deleteProduct(int productID)
        {
            Product tbd = _context.Product.Where<Product>(product => product.ID == productID).Single();
            _context.Product.Remove(tbd);
        }

        public void Dispose()
        {
        }

        public Product getProductByID(int productID)
        {
            return _context.Product.Where<Product>(product => product.ID == productID).Single();
        }

        public IEnumerable<Product> getProducts(ProductQuery query, int page, int results, out int total)
        {   IQueryable<Product> productQuery = _context.Product.Where<Product>(query.asExpression());
            int pageSkip = page > 0 ? page : 0;
            int resultSkip = results > 0 ? results : 25;
            total = productQuery.Count();
            return productQuery.Skip(page * resultSkip).Take(resultSkip).ToList();
        }

        
        public void insertProduct(Product product, ProductType type, Brand brand )
        {
            if (type != null && type.ID == 0) {
                _context.ProductType.Add(type);
                product.ProductType = type;
            }
            if (brand != null && brand.ID == 0 ) {
                _context.Brand.Add(brand);
                product.Brand = brand;
            }
            _context.ProductType.Add(type);
            _context.Add(product);
        }

        public void insertProduct(Product product, ProductType type) {
            insertProduct(product, type, null);
        }
        public void insertProduct(Product product , Brand brand) {
            insertProduct(product, null, brand);
        }
        public void insertProduct(Product product) {
            insertProduct(product, null, null);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void updateProduct(Product product)
        {
            _context.Product.Update(product);
        }
    }
}
