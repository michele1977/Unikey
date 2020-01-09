using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public class ADO_UserRepository : IUserRepository
    {
        public void Delete(int id)
        {
            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("delete from users where ID = @id", connection))
                {
                    connection.Open();

                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = id
                    };

                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetUsersList()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from users", connection))
                {
                    connection.Open();
                    SqlDataReader reader =  command.ExecuteReader();

                    while(reader.Read())
                    {
                        users.Add(new User
                        {
                            ID = reader.GetFieldValue<int>(reader.GetOrdinal("ID")),
                            Username = reader.GetFieldValue<string>(reader.GetOrdinal("Username")),
                            Password = reader.GetFieldValue<string>(reader.GetOrdinal("Password")),
                            Email = reader.GetFieldValue<string>(reader.GetOrdinal("Email"))
                        });
                    }
                }
            }

            return users;
        }

        public void Save(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("insert into users values (@Email, @Username, @Password)", connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("@Username", user.Username));
                    command.Parameters.Add(new SqlParameter("@Password", user.Password));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
