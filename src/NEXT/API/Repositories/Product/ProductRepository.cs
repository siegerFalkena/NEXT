using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;
using NEXT.API.Query;
using NEXT.API.Resource;
using NEXT.API.Mappers;

namespace NEXT.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NEXTContext _context;
        private ProductMapping productMapping;
        public ProductRepository(NEXTContext context) {
            this._context = context;
            productMapping = new ProductMapping();
        }

        public void deleteProduct(int productID)
        {
            DB.Models.Product tbd = _context.Product.Where<DB.Models.Product>(product => product.ID == productID).Single();
            _context.Product.Remove(tbd);
        }

        public void Dispose()
        {
        }

        public API.Resource.Product getProductByID(int productID)
        {
             DB.Models.Product  product = _context.Product.Where<DB.Models.Product>(dbProduct => dbProduct.ID == productID).Single();
            return productMapping.map(product);
        }

        public IEnumerable<API.Resource.Product> getProducts(ProductQuery query, int page, int results, out int total)
        {
            Expression<Func<DB.Models.Product, bool>> queryAsExpre = query.asExpression();
            total = 0;
                //_context.Product.Where<Product>(queryAsExpre).Where(product => true).Count();
            
            IQueryable<DB.Models.Product> ProductQuery = _context.Product.Where<DB.Models.Product>(queryAsExpre);
            IQueryable<DB.Models.Brand> BrandQuery = _context.Brand;
            ProductQuery = ProductQuery.Join(_context.Brand, (i) => i.BrandID, o => o.ID, (o, i) => o.BrandID == i.ID ? o.Brand = i : o.Brand = null;);
            ProductQuery = ProductQuery.Join()
            
            int pageSkip = page > 0 ? page : 0;
            int resultSkip = results > 0 ? results : 25;
            ProductQuery = query.getOrdering(ProductQuery);
            IEnumerable<DB.Models.Product> products = ProductQuery.Skip(page * resultSkip).Take(resultSkip).ToList();

            List<API.Resource.Product> retList = new List<Resource.Product>();
            foreach (DB.Models.Product prod in products) {
                API.Resource.Product newprod = productMapping.map(prod);
                retList.Add(newprod);
            }
            return retList;
        }

        
        public void insertProduct(DB.Models.Product Product, DB.Models.ProductType type, DB.Models.Brand brand )
        {
            if (type != null && type.ID == 0) {
                _context.ProductType.Add(type);
                Product.ProductType = type;
            }
            if (brand != null && brand.ID == 0 ) {
                _context.Brand.Add(brand);
                Product.Brand = brand;
            }
            _context.ProductType.Add(type);
            _context.Add(Product);
        }

        public void insertProduct(DB.Models.Product Product, DB.Models.ProductType type) {
            insertProduct(Product, type, null);
        }
        public void insertProduct(DB.Models.Product product , DB.Models.Brand brand) {
            insertProduct(product, null, brand);
        }
        public void insertProduct(DB.Models.Product product) {
            insertProduct(product, null, null);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void updateProduct(DB.Models.Product product)
        {
            _context.Product.Update(product);
        }
    }
}
