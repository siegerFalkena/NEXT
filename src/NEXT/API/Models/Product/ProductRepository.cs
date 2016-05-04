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
        private ApplicationContext _context;

        public ProductRepository(ApplicationContext context) {
            this._context = context;
        }

        public void deleteProduct(int productID)
        {
            Product tbd = _context.products.Where<Product>(product => product.productID == productID).Single();
            _context.products.Remove(tbd);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Product getProductByID(int productID)
        {
           return _context.products.Where<Product>(product => product.productID == productID).Single();
        }

        public IEnumerable<Product> getProducts(ProductQuery query,int page, int results)
        {
            return _context.products.Where<Product>(query.asExpression()).Skip(page*results).Take(results).ToList();
        }

        public void insertProduct(Product product)
        {
            _context.Add(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void updateProduct(Product product)
        {
            _context.products.Update(product);
        }
    }
}
