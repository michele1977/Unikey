using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        public List<User> GetUsersList();
        public void Save(User user);
        public void Delete(int id);
    }
}
