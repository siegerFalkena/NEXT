using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.API.Query;

namespace NEXT.API.Models
{
    public class CompanyQuery
    {
        Company example = new Company();

        int? ID = null;
        bool? isActive = null;
        string name = null;



        public Expression<Func<Company, bool>> asExpression() {
            Expression<Func<Company, bool>> query =  (Company => true);
            if (ID != null) {
                Expression<Func<Company, bool>> firstnameQuery = (Company => Company.Name == name);
                query = PredicateBuilder.And(query, firstnameQuery);
            }

            if (isActive != null)
            {
                Expression<Func<Company, bool>> lastnameQuery = (Company => Company.IsActive == isActive);
                query = PredicateBuilder.And(query, lastnameQuery);
            }
            return query;
        }
    }
}
