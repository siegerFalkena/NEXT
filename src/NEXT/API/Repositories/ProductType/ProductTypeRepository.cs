using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.DB.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private NEXTContext _context;

        public ProductTypeRepository(NEXTContext context) {
            this._context = context;
        }

        public void deleteProductType(int ProductTypeID)
        {
            ProductType tbd = _context.ProductType.Where<ProductType>(ProductType => ProductType.ID == ProductTypeID).Single();
            _context.ProductType.Remove(tbd);
        }

        public void Dispose()
        {
            
        }

        public ProductType getProductTypeByID(int ProductTypeID)
        {
            return _context.ProductType.Where<ProductType>(ProductType => ProductType.ID == ProductTypeID).Single();
        }

        public IEnumerable<ProductType> getProductTypes(ProductTypeQuery query, int page, int results)
        {
            return _context.ProductType.Where<ProductType>(query.asExpression()).Skip(page * results).Take(results).ToList();
        }

        public void insertProductType(ProductType ProductType)
        {
            _context.ProductType.Add(ProductType);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void updateProductType(ProductType ProductType)
        {
            _context.ProductType.Update(ProductType);
        }
    }
}
