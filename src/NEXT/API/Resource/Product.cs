using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEXT.API.Resource
{
    public class Product : IResource
    {


        public int productID { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public string ExternalProductIdentifier { get; set; }
        public DateTime? LastModified { get; set; }
        public int? LastModifiedBy { get; set; } = null;
        public string SKU { get; set; }
        public string description { get; set; } = null;


        public ProductType type { get; set; } = null;
        public Brand brand { get; set; } = null;
        public Channel channel { get; set; } = null;
        public ICollection<ProductAttribute> attributes;


        public string Name { get; set; } = null;


        public string toJson()
        {
            return "/api/product/" + productID;
        }

        public string asRelation()
        {
            throw new NotImplementedException();
        }
    }
}
