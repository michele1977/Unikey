using Domain;
using eCommerce.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        readonly Context context = new Context();

        public List<User> GetUsersList()
        {
            return context.Users.ToList();
        }

        public void Save(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Users.Remove(context.Users.FirstOrDefault(u => u.ID == id));
            context.SaveChanges();
        }
    }
}

