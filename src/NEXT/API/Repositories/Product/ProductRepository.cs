using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using NEXT.DB.Models;
using NEXT.API.Query;
using NEXT.API.Resource;
using AutoMapper;
using AutoMapper.Mappers;

namespace NEXT.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NEXTContext _context;
        private IMappingConfigProvider _mapConfig;
        private IMapper mapper;

        public ProductRepository(NEXTContext context, IMappingConfigProvider mapConfig) {
            this._context = context;
            this._mapConfig = mapConfig;
            mapper = _mapConfig.getConfig().CreateMapper();
        }


        public void Dispose() {
        }

        public void deleteProduct(int productID)
        {
            DB.Models.Product tbd = _context.Product.Where<DB.Models.Product>(product => product.ID == productID).Single();
            _context.Product.Remove(tbd);
        }


        public API.Resource.Product getProductByID(int productID)
        {
             DB.Models.Product  product = _context.Product.Where<DB.Models.Product>(dbProduct => dbProduct.ID == productID).Single();
            return mapper.Map<DB.Models.Product, API.Resource.Product>(product);
        }


        public IEnumerable<API.Resource.Product> getProducts(ProductQuery query, out int total)
        {
            Expression<Func<DB.Models.Product, bool>> queryAsExpre = query.asExpression();
            IQueryable<DB.Models.Product> productQuery = _context.Product
            .Include(p => p.Brand)
            .Include(p => p.ChannelProduct)
            .Include(p => p.ProductAttributeOption)
            .Include(p => p.ProductAttributeValue)
            .Include(p => p.ProductType)
            .Include(p => p.RelatedProduct)
            .Include(p => p.RelatedProductNavigation)
            .Include(p => p.VendorProduct)
            .Where<DB.Models.Product>(queryAsExpre);
            IQueryable<DB.Models.Product> newQuery = query.getOrdering(productQuery);
            IEnumerable<DB.Models.Product> products = newQuery
                .Skip(query.page * query.results)
                .Take(query.results)
                //.Select<IEnumerable<DB.Models.Product>, IEnumerable<API.Resource.Product>>((x)=> mapper.Map<IEnumerable<API.Resource.Product>>(x))
                .ToList();

            total = productQuery.Count();

            List<API.Resource.Product> retList = new List<Resource.Product>();
            foreach (DB.Models.Product prod in products) {
                API.Resource.Product newprod = mapper.Map<API.Resource.Product>(prod);
                retList.Add(newprod);
            }
            return retList;
        }

        
        public void insertProduct(API.Resource.Product product, API.Resource.ProductType type, API.Resource.Brand brand )
        {
            DB.Models.Product insertProduct = mapper.Map<DB.Models.Product>(product);
            _context.Product.Add(insertProduct);
            if (type != null && type.ID == 0) {
                _context.ProductType.Add(mapper.Map<DB.Models.ProductType>(type));
            }
            if (brand != null && brand.brandID == 0) {
                _context.Brand.Add(mapper.Map<DB.Models.Brand>(brand));
            }
        }


        public void insertProduct(API.Resource.Product Product, API.Resource.ProductType type) {
            insertProduct(Product, type, null);
        }


        public void insertProduct(API.Resource.Product product , API.Resource.Brand brand) {
            insertProduct(product, null, brand);
        }


        public void insertProduct(API.Resource.Product product) {
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