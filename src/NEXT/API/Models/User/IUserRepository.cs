using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Models;

namespace NEXT.API.Repositories
{
    public interface IUserRepository : IDisposable
    {
        void createUser(User user);
        void deleteUser(int ID);
        User getUserByID(int ID);
        void updateUser(User user);
        List<User> userQuery(UserQuery query, int results, int skipPages);
        void save();
    }
}
