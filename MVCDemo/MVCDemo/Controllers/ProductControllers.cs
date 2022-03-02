using MVCDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo.Controllers
{
    internal class ProductControllers
    {
        SqlConnection _connection = new SqlConnection(@"Integrated Security = SSPI; Initial Catalog = Northwind; Data Source = BORA;");
        public List<Product> GetAll(string categoryName)
        {
            List<Product> products = new List<Product>();
            ConnectionControl();
            SqlCommand command = new SqlCommand($"Select * from Northwind.dbo.Products p inner join Northwind.dbo.Categories c on p.CategoryId = c.CategoryId where c.CategoryName = '{categoryName}'", _connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Product product = new Product
                {
                    ProductName = dataReader["ProductName"].ToString(),
                    CategoryName = dataReader["CategoryName"].ToString()
                };
                products.Add(product);
            }

            dataReader.Close();
            _connection.Close();

            return products;
        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
    }
}
