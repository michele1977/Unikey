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
    public class ADO_ProductRepository : IProductRepository
    {
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("delete from products where ID = @id", connection))
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

        public Product Details(int id)
        {
            Product product = new Product();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from products where ID = @id", connection))
                {
                    connection.Open();

                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = "@id",
                        Value = id
                    };

                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        product.ID = reader.GetFieldValue<int>(reader.GetOrdinal("ID"));
                        product.Price = reader.GetFieldValue<double>(reader.GetOrdinal("Price"));
                        product.Color = reader.GetFieldValue<string>(reader.GetOrdinal("Color"));
                        product.Description = reader.GetFieldValue<string>(reader.GetOrdinal("Description"));
                        product.Weigth = reader.GetFieldValue<double>(reader.GetOrdinal("Weigth"));
                    }
                }
            }

            return product;
        }

        public List<Product> GetProductsList()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from products", connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Product 
                        {
                            ID = reader.GetFieldValue<int>(reader.GetOrdinal("ID")),
                            Price = reader.GetFieldValue<double>(reader.GetOrdinal("Price")),
                            Color = reader.GetFieldValue<string>(reader.GetOrdinal("Color")),
                            Description = reader.GetFieldValue<string>(reader.GetOrdinal("Description")),
                            Weigth = reader.GetFieldValue<double>(reader.GetOrdinal("Weigth"))
                        });
                    }
                }
            }

            return products;
        }

        public void Save(int id, Product product)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("insert into products values (@Price, @Color, @Description, @Weigth, @Brand_ID)", connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("@Price", product.Price));
                    command.Parameters.Add(new SqlParameter("@Color", product.Color));
                    command.Parameters.Add(new SqlParameter("@Description", product.Description));
                    command.Parameters.Add(new SqlParameter("@Weigth", product.Weigth));
                    command.Parameters.Add(new SqlParameter("@Brand_ID", id));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("update products set Price = @Price, Color = @Color, Description = @Description, Weigth = @Weigth where ID = @id", connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("@Price", product.Price));
                    command.Parameters.Add(new SqlParameter("@Color", product.Color));
                    command.Parameters.Add(new SqlParameter("@Description", product.Description));
                    command.Parameters.Add(new SqlParameter("@Weigth", product.Weigth));
                    command.Parameters.Add(new SqlParameter("@id", product.ID));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
