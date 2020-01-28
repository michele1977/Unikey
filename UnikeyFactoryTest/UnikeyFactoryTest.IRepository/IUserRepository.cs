using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.IRepository
{
    public interface IUserRepository
    {
        Task<bool> FindUser(User user);
        Task<int> GetUserIdByUsername(User user);
        void InsertUser(User user);
    }
}
