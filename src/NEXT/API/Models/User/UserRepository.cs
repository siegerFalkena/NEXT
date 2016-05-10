using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.API.Repositories;

namespace NEXT.API.Models
{
    public class UserRepository : IUserRepository
    {
        private NEXTContext context;

        public UserRepository(NEXTContext context) {
            this.context = context;
        }
        
        void IUserRepository.createUser(User user)
        {
            context.User.Add(user);
        }

        void IUserRepository.deleteUser(int ID)
        {
            User tempUser =  context.User.Where<User>( user=> user.ID == ID).Single();
            context.User.Remove(tempUser);
        }

        User IUserRepository.getUserByID(int ID)
        {
            return context.User.Where(user => user.ID == ID).Single();
        }

        void IUserRepository.updateUser(User user)
        {
            context.User.Update(user);
        }

        List<User> IUserRepository.userQuery(UserQuery query, int results, int skipPages)
        {
            IQueryable<User> queryObject = context.User.Where(query.asExpression());
            if ( skipPages > 0) {
                queryObject = queryObject.Skip(skipPages * results);
            }
            return queryObject.Take(results).ToList();
        }

        void IUserRepository.save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
