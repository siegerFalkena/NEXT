using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private NEXTContext _context;

        public BrandRepository(NEXTContext context) {
            this._context = context;
        }

        public void deleteBrand(int BrandID)
        {
            Brand tbd = _context.Brand.Where<Brand>(Brand => Brand.ID == BrandID).Single();
            _context.Brand.Remove(tbd);
        }

        public void Dispose()
        {
            
        }

        public Brand getBrandByID(int BrandID)
        {
            return _context.Brand.Where<Brand>(Brand => Brand.ID == BrandID).Single();
        }

        public IEnumerable<Brand> getBrands(BrandQuery query, int page, int results)
        {
            return _context.Brand.Where<Brand>(query.asExpression()).Skip(page * results).Take(results).ToList();
        }

        public void insertBrand(Brand brand)
        {
            _context.Brand.Add(brand);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void updateBrand(Brand Brand)
        {
            _context.Brand.Update(Brand);
        }
    }
}
