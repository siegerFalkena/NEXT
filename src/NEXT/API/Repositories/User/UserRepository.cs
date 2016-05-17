using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;

namespace NEXT.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private NEXTContext context;

        public UserRepository(NEXTContext context) {
            this.context = context;
        }
        
        void IUserRepository.createUser(API.Resource.User user)
        {
            context.User.Add(user);
        }

        void IUserRepository.deleteUser(int ID)
        {
            User tempUser =  context.User.Where<User>( user=> user.ID == ID).Single();
            tempUser.IsDeleted = true;
            //context.User.Remove(tempUser);
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

        User IUserRepository.getUserByName(string name) {
            return context.User.Where(user => user.Username.Equals(name)).Single();
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
