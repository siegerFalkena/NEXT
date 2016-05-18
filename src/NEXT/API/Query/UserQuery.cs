using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public class UserQuery : AbstractQuery
    {
        public int? email = null;
        public string firstNameContains = null;
        public string lastNameContains = null;
        public bool? isActive = null;
        public bool? isDeleted = null;
        public string lastName = null;
        public string userName = null;



        public Expression<Func<User, bool>> asExpression() {
            Expression<Func<User, bool>> query =  (user => true);
            if (firstNameContains != null) {
                Expression<Func<User, bool>> firstnameQuery = (user => user.Firstname == firstNameContains);
                query = PredicateBuilder.And(query, firstnameQuery);
            }

            if (lastNameContains != null)
            {
                Expression<Func<User, bool>> lastnameQuery = (user => user.Lastname == lastName);
                query = PredicateBuilder.And(query, lastnameQuery);
            }

            if (isActive != null)
            {
                Expression<Func<User, bool>> activeQuery = (user => user.IsActive == isActive);
                query = PredicateBuilder.And(query, activeQuery);
            }

            if (isDeleted != null)
            {
                Expression<Func<User, bool>> deletedQuery = (user => user.IsDeleted == isDeleted);
                query = PredicateBuilder.And(query, deletedQuery);
            }

            if (userName != null)
            {
                Expression<Func<User, bool>> usernameContainsQuery = (user => user.Username == userName);
                query = PredicateBuilder.And(query, usernameContainsQuery);
            }

            return query;
        }
    }
}
