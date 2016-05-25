using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Resource;

namespace NEXT.API.Repositories
{
    public interface IUserRepository : IDisposable
    {
        void createUser(User user);
        void deleteUser(int ID);
        User getUserByID(int ID);
        User getUserByName(string name);
        void updateUser(User user);
        List<User> userQuery(UserQuery query);
        void save();
    }
}
