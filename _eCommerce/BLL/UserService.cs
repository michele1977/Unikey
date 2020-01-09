using Domain;
using Repository;
using System.Collections.Generic;

namespace BLL
{
    public enum ErrorState
    {
        Yes,
        No,
        Empty
    }
    public class UserService
    {
        readonly IUserRepository repository;

        public UserService(IUserRepository param)
        {
            repository = param;
        }
         
        public ErrorState Save(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) && string.IsNullOrWhiteSpace(user.Password) && string.IsNullOrWhiteSpace(user.Email))
            {
                return ErrorState.Empty;
            }
            else if(string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Email))
            {
                return ErrorState.No;
            }
            else
            {
                foreach(var _user in repository.GetUsersList())
                {
                    if (_user.Email == user.Email) return ErrorState.No;
                }
                repository.Save(user);
                return ErrorState.Yes;
            }
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<User> GetUsersList()
        {
            return repository.GetUsersList();
        }
    }
}
