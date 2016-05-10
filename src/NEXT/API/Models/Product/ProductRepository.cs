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

        public IEnumerable<Product> getProducts(ProductQuery query, int page, int results)
        {
            return _context.Product.Where<Product>(query.asExpression()).Skip(page * results).Take(results).ToList();
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
            _context.Product.Update(product);
        }
    }
}
