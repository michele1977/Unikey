using System.Threading.Tasks;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.IService
{
    public interface IUserService
    {
        Task<bool> IsUser(User user);
        Task<int> GetUserIdByUsername(User user);
        void InsertUser(User user);
        void Dispose();
    }
}