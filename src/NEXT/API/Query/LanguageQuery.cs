using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Query
{
    public class LanguageQuery : AbstractQuery
    {
        public string Name { get; set; } = null;
        public bool ascending = true;
        public string orderBy = null;

        public Expression<Func<Language, bool>> asExpression()
        {
            Expression<Func<Language, bool>> query = (ProductType => true);
            if (Name != null)
            {
                query = PredicateBuilder.And(query, (language => language.Name.Contains(Name)));
            }
            return query;
        }

        public IQueryable<Language> getOrdering(IQueryable<Language> queryable)
        {
            if (orderBy != null)
            {
                if (orderBy.Equals("Name")) return ascending ? queryable.OrderBy(language => language.Name) : queryable.OrderByDescending(language => language.Name);
            }
            return queryable;
        }
    }
}
