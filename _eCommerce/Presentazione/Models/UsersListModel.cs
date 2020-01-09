using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentazione.Models
{
    public class UsersListModel
    {
        public int ID { get; set; }
        public List<User> UsersList { get; set; }
    }
}