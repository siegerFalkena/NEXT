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

//TODO validation
namespace NEXT.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NEXTContext _context;
        private IMappingConfigProvider _mapConfig;
        private IMapper mapper;


        //TODO pagination and resultselection on related
        public ProductRepository(NEXTContext context, IMappingConfigProvider mapConfig)
        {
            this._context = context;
            this._mapConfig = mapConfig;
            mapper = _mapConfig.getConfig().CreateMapper();
        }


        public void Dispose()
        {
            //nothing to dispose
        }

        public void deleteProduct(int productID)
        {
            DB.Models.Product tbd = _context.Product.Where<DB.Models.Product>(product => product.ID == productID).Single();
            _context.Product.Remove(tbd);
        }


        public ICollection<API.Resource.Product> getChildren(int productID, int results, int page)
        {
            int queryResults = results == 0 ? 25 : results;
            ICollection<DB.Models.Product> products = _context.Product.Include(p => p.InverseParentProduct).Include(p => p.Brand).Include(p => p.ProductType)
                .Where(p => p.ParentProductID == productID).ToList();
            return mapper.Map<ICollection<DB.Models.Product>, ICollection<API.Resource.Product>>(products);
        }


        public API.Resource.Product getProductByID(int productID)
        {
            IQueryable<DB.Models.Product> productQuery = _context.Product
           .Include(p => p.Brand)
           .Include(p => p.ChannelProduct)
           .ThenInclude(cp => cp.Channel)
           .Include(p => p.ProductType)
           .Include(p => p.RelatedProduct)
           .Include(p => p.RelatedProductNavigation)
           .Include(p => p.VendorProduct)
           .ThenInclude(vp => vp.Vendor)
           .Include(p => p.ProductAttributeOption)
           .ThenInclude(pao => pao.AttributeOption)
           .ThenInclude(pao => pao.Attribute)
           .ThenInclude(pao => pao.AttributeType)
           .Include(p => p.ProductAttributeValue)
           .ThenInclude(pao => pao.Attribute)
           .ThenInclude(pao => pao.AttributeType)
           .Include(p => p.ParentProduct)
           .Where<DB.Models.Product>(dbProduct => dbProduct.ID == productID);
            DB.Models.Product product = productQuery.FirstOrDefault();
            return product != null ? mapper.Map<DB.Models.Product, API.Resource.Product>(product) : null;
        }


        public IEnumerable<API.Resource.Product> getProducts(ProductQuery query, out int total)
        {
            Expression<Func<DB.Models.Product, bool>> queryAsExpre = query.asExpression();
            IQueryable<DB.Models.Product> productQuery = _context.Product
          .Include(p => p.Brand)
        .Include(p => p.ChannelProduct)
        .ThenInclude(cp => cp.Channel)
        .Include(p => p.ProductType)
        .Include(p => p.RelatedProduct)
        .Include(p => p.RelatedProductNavigation)
        .Include(p => p.VendorProduct)
        .ThenInclude(vp => vp.Vendor)
        .Include(p => p.ProductAttributeOption)
        .ThenInclude(pao => pao.AttributeOption)
        .ThenInclude(pao => pao.Attribute)
        .ThenInclude(pao => pao.AttributeType)
        .Include(p => p.ProductAttributeValue)
        .ThenInclude(pao => pao.Attribute)
        .ThenInclude(pao => pao.AttributeType)
        .Include(p => p.ParentProduct)
            .Where<DB.Models.Product>(queryAsExpre);
            IQueryable<DB.Models.Product> newQuery = query.getOrdering(productQuery);
            List<DB.Models.Product> products = newQuery
                .Skip(query.page * query.results)
                .Take(query.results)
                .ToList();

            total = productQuery.Count();

            List<API.Resource.Product> retList = new List<Resource.Product>();
            foreach (DB.Models.Product prod in products)
            {
                API.Resource.Product newprod = mapper.Map<API.Resource.Product>(prod);
                retList.Add(newprod);
            }
            return retList;
        }


        public void insertProduct(API.Resource.Product product, API.Resource.ProductType type, API.Resource.Brand brand)
        {
            DB.Models.Product insertProduct = mapper.Map<DB.Models.Product>(product);
            _context.Product.Add(insertProduct);
            if (type != null && type.ID == 0)
            {
                _context.ProductType.Add(mapper.Map<DB.Models.ProductType>(type));
            }
            if (brand != null && brand.brandID == 0)
            {
                _context.Brand.Add(mapper.Map<DB.Models.Brand>(brand));
            }
        }


        public void insertProduct(API.Resource.Product Product, API.Resource.ProductType type)
        {
            insertProduct(Product, type, null);
        }


        public void insertProduct(API.Resource.Product product, API.Resource.Brand brand)
        {
            insertProduct(product, null, brand);
        }


        public void insertProduct(API.Resource.Product product)
        {
            insertProduct(product, null, null);
        }


        public API.Resource.Product createProduct(API.Resource.Product newProduct)
        {
            DB.Models.Product dbProduct = mapper.Map<DB.Models.Product>(newProduct);
            dbProduct.Created = DateTime.Now;
            dbProduct.LastModified = DateTime.Now;

            _context.Product.Add(dbProduct);
            _context.SaveChanges();

            DB.Models.Product savedProduct = _context.Product.Where(p => p.SKU == newProduct.SKU).SingleOrDefault();
            if (savedProduct == null) { return null; }
            return mapper.Map<API.Resource.Product>(savedProduct);
        }


        public void Save()
        {
            _context.SaveChanges();
        }


        public void updateProduct(API.Resource.Product product)
        {
            DB.Models.Product upProduct = mapper.Map<API.Resource.Product, DB.Models.Product>(product);
            DB.Models.Product frProduct = _context.Product.Where(p => p.ID == upProduct.ID).SingleOrDefault();
            frProduct.SKU = upProduct.SKU;
            frProduct.ExternalProductIdentifier = upProduct.ExternalProductIdentifier;
            frProduct.ProductTypeID = upProduct.ProductTypeID;
            frProduct.BrandID = upProduct.BrandID;
            _context.Update(frProduct, GraphBehavior.SingleObject);
            _context.SaveChanges();
        }


        public ICollection<API.Resource.ProductAttribute> getAttributes(int productID)
        {
            List<API.Resource.ProductAttribute> attributes = new List<Resource.ProductAttribute>();
            DB.Models.Product product = _context.Product.Include(p => p.ProductAttributeOption)
                .ThenInclude(p => p.AttributeOption).ThenInclude(ap => ap.Attribute).ThenInclude(at => at.AttributeType)
                .Include(p => p.ProductAttributeValue).ThenInclude(p => p.Attribute).ThenInclude(at => at.AttributeType).Where(p => p.ID == productID).SingleOrDefault();
            if (product == null) { return attributes; };
            foreach (DB.Models.ProductAttributeValue pod in product.ProductAttributeValue)
            {
                attributes.Add(mapper.Map<API.Resource.ProductAttribute>(pod));
            }
            foreach (DB.Models.ProductAttributeOption pod in product.ProductAttributeOption)
            {
                attributes.Add(mapper.Map<API.Resource.ProductAttribute>(pod));
            }
            return attributes;
        }


        public Resource.Product getParent(int id)
        {
            DB.Models.Product product = _context.Product.Include(p => p.ParentProduct).Where(p => p.ID == id).SingleOrDefault();
            API.Resource.Product retProduct = null;
            if (product != null)
            {
                retProduct = mapper.Map<API.Resource.Product>(product);
            }
            return retProduct;
        }

        public Resource.Brand getBrand(int id)
        {
            //not null
            DB.Models.Brand productBrand = _context.Product.Include(p => p.Brand).Where(p => p.ID == id).Select(p => p.Brand).SingleOrDefault();
            _context.Dispose();
            return mapper.Map<API.Resource.Brand>(productBrand);

        }


        public ICollection<Resource.Product> getRelatedProducts(int id, int results, int page)
        {
            DB.Models.Product product = _context.Product.Include(p => p.RelatedProduct).ThenInclude(p => p.Product).Where(p => p.ID == id).SingleOrDefault();
            if (product == null) { return null; };
            IEnumerable<DB.Models.Product> relatedProducts = product.RelatedProduct.Select(p => p.Product);
            if (relatedProducts.Count() == 0) { return null; }
            ICollection<DB.Models.Product> Products = relatedProducts.ToList();
            return mapper.Map<ICollection<API.Resource.Product>>(Products);
        }


        //NOTE inverse of related?
        public ICollection<Resource.Product> getRelatedNavigationProducts(int id, int results, int page)
        {
            int queryResults = results > 0 ? results : 25;
            DB.Models.Product product = _context.Product.Include(p => p.RelatedProduct).ThenInclude(p => p.Product).Where(p => p.ID == id).SingleOrDefault();
            if (product == null) { return null; };
            IEnumerable<DB.Models.Product> relatedProducts = product.RelatedProduct.Select(p => p.Product).Skip(queryResults * page).Take(results);
            if (relatedProducts.Count() == 0) { return null; }
            ICollection<DB.Models.Product> Products = relatedProducts.ToList();
            return mapper.Map<ICollection<API.Resource.Product>>(Products);
        }


        public ICollection<API.Resource.Vendor> getVendors(int productID)
        {
            DB.Models.Product product = _context.Product.Include(p => p.VendorProduct).ThenInclude(vp => vp.Vendor).Where(p => p.ID == productID).SingleOrDefault();
            if (product == null) { return null; };
            ICollection<DB.Models.Vendor> vendors = product.VendorProduct.Select(vp => vp.Vendor).ToList();
            return mapper.Map<ICollection<API.Resource.Vendor>>(vendors);
        }


        public void removeVendor(int productID, int vendorID)
        {
            ICollection<DB.Models.VendorProduct> product = _context.Product.Include(p => p.VendorProduct).SelectMany(p => p.VendorProduct).ToList();
            foreach (VendorProduct vp in product)
            {
                _context.VendorProduct.Remove(vp);
            }
            _context.SaveChangesAsync();
        }

        public ICollection<API.Resource.Channel> getChannels(int productID)
        {
            DB.Models.Product product = _context.Product.Include(p => p.ChannelProduct).ThenInclude(vp => vp.Channel).Where(p => p.ID == productID).SingleOrDefault();
            if (product == null) { return null; };
            ICollection<DB.Models.Channel> channels = product.ChannelProduct.Select(vp => vp.Channel).ToList();
            return mapper.Map<ICollection<API.Resource.Channel>>(channels);
        }

        public Resource.ProductType getType(int productID)
        {
            //TODO not null defense
            DB.Models.ProductType productBrand = _context.Product.Include(p => p.ProductType).Where(p => p.ID == productID).Select(p => p.ProductType).SingleOrDefault();
            return mapper.Map<API.Resource.ProductType>(productBrand);
        }

        public int setBrand(int productID, API.Resource.Brand brand)
        {
            DB.Models.Brand dbBrand = mapper.Map<DB.Models.Brand>(brand);
            DB.Models.Product product = _context.Product.Where(p => p.ID == productID).SingleOrDefault();
            if (product == null) { return 0; }
            product.BrandID = brand.brandID;
            _context.Update(product);
            return _context.SaveChanges();
        }

        public int setType(int productID, Resource.ProductType type)
        {
            DB.Models.ProductType dbProductType = mapper.Map<DB.Models.ProductType>(type);
            DB.Models.Product product = _context.Product.Where(p => p.ID == productID).SingleOrDefault();
            if (product == null) { return 0; }
            _context.Attach(dbProductType);
            product.ProductType = dbProductType;
            _context.Update(product);
            return _context.SaveChanges();
        }

        public Resource.ProductAttribute createOrUpdateAttribute(Resource.ProductAttribute attribute)
        {
            DB.Models.ProductAttributeValue pa = _context.ProductAttributeValue.Where(pav => pav.AttributeID == attribute.AttributeID && pav.VendorID == attribute.VendorID && pav.LanguageID == attribute.LanguageID && pav.ProductID == attribute.ProductID).SingleOrDefault();

            if (pa != null)
            {
                _context.ProductAttributeValue.Attach(pa);
                pa.VendorID = attribute.VendorID;
                pa.ProductID = attribute.ProductID;
                pa.LanguageID = attribute.LanguageID;
                _context.ProductAttributeValue.Update(pa);
            }
            else
            {
                pa = mapper.Map<DB.Models.ProductAttributeValue>(attribute);
                _context.ProductAttributeValue.Add(pa);
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                _context.SaveChanges();
                throw concurrencyException;
            }
            return mapper.Map<Resource.ProductAttribute>(pa);
        }
    }
}