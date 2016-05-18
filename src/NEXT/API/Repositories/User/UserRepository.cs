using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;
using AutoMapper;
namespace NEXT.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private NEXTContext context;
        private IMapper mapper;
        public UserRepository(NEXTContext context, IMappingConfigProvider mapperConfigProvider)
        {
            this.context = context;
            mapper = mapperConfigProvider.getConfig().CreateMapper();

        }

        void IUserRepository.createUser(API.Resource.User user)
        {
            DB.Models.User toBeAdded = mapper.Map<DB.Models.User>(user);
            context.User.Add(toBeAdded);
        }

        void IUserRepository.deleteUser(int ID)
        {
            User tempUser = context.User.Where<User>(user => user.ID == ID).Single();
            tempUser.IsDeleted = true;
            //context.User.Remove(tempUser);
        }

        API.Resource.User IUserRepository.getUserByID(int ID)
        {
            DB.Models.User gotUser = context.User.Where(user => user.ID == ID).Single();
            return mapper.Map<API.Resource.User>(gotUser);
        }

        void IUserRepository.updateUser(API.Resource.User user)
        {
            DB.Models.User update = mapper.Map<DB.Models.User>(user);
            context.User.Update(update);
        }

        List<API.Resource.User> IUserRepository.userQuery(UserQuery query)
        {
            IQueryable<User> queryObject = context.User.Where(query.asExpression());
            queryObject = queryObject.Skip(query.page * query.results);
            List<DB.Models.User> dbUsers = queryObject.Take(query.results).ToList();
            List<API.Resource.User> apiUsers = new List<Resource.User>(dbUsers.Count());
            foreach (DB.Models.User dbUser in dbUsers) {
                apiUsers.Add(mapper.Map<API.Resource.User>(dbUser));
            }
            return apiUsers;
        }

        API.Resource.User IUserRepository.getUserByName(string name)
        {
            DB.Models.User byName = context.User.Where(user => user.Username.Equals(name)).Single();
            return mapper.Map<API.Resource.User>(byName);
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
