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
    public class ADO_BrandRepository : IBrandRepository
    {
        public Brand GetBrand(int id)
        {
            Brand brand = new Brand();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from brands where ID = @id", connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        brand.ID = reader.GetFieldValue<int>(reader.GetOrdinal("ID"));
                        brand.Description = reader.GetFieldValue<string>(reader.GetOrdinal("Description"));
                    }
                }
            }

            return brand;
        }

        public List<Brand> GetBrands()
        {
            List<Brand> brands = new List<Brand>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from brands", connection))
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        brands.Add(new Brand
                        {
                            ID = reader.GetFieldValue<int>(reader.GetOrdinal("ID")),
                            Description = reader.GetFieldValue<string>(reader.GetOrdinal("Description"))
                        });
                    }
                }
            }

            return brands;
        }

        public List<Product> GetProductsForBrand(int id)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["eCommerce"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from products where Brand_ID = @Brand_ID", connection))
                {
                    connection.Open();

                    command.Parameters.Add(new SqlParameter("@Brand_ID", id));

                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
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
    }
}
