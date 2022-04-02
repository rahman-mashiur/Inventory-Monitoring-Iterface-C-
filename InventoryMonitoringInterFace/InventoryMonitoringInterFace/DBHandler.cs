using System;
using System.Collections;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows;
namespace InventoryMonitoringInterFace
{
    public class DBHandler
    {
        public MySqlConnection connection;
        public DBHandler()
        {
                String connectionString = ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString;
                connection = new MySqlConnection(connectionString);
        }
        public LoginData getLoginInfo(String username, String password)
        {
            string query = "SELECT * FROM login where uid='"+username+"' and password='" + password + "'";
            this.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            LoginData ld = new LoginData();
            if (dataReader.Read())
            {
                ld.uid =(String) dataReader["uid"];
                ld.pass = (String)dataReader["password"];
                ld.uType = (String)dataReader["user_type"];
            }
            else
            {
                ld.errormsg = "User does not exists";
            }
            dataReader.Close();
            this.CloseConnection();
            return ld;
        }
        public ArrayList search(String productName)
        {
            try
            {
                string query = "SELECT * FROM products where product_name='" + productName + "'";
                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                ArrayList prductlist = new ArrayList();
                while (reader.Read())
                {
                    Products p = new Products();
                    p.productName = (string)reader["product_name"];
                    p.productQuantity = (int)reader["product_quantity"];
                    p.productPrice = (double)reader["product_price"];
                    p.productCategory = (String)reader["product_category"];
                    prductlist.Add(p);
                }
                this.CloseConnection();
                return prductlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertData(String productName, int quantity, double price, String category)
        {
            string query = "INSERT INTO products (product_name, product_quantity, product_price, product_category)" +
                "VALUES ('" + productName + "','" + quantity + "','" + price + "','" + category + "')";
            try
            {
                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to insert.");
            }
        }
        public int deleteData(int productId)
        {
            try
            {
                string query = "DELETE FROM products WHERE product_id='"+productId+"'";
                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int total = cmd.ExecuteNonQuery();
                this.CloseConnection();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateData(int productId, String productName, int quantity, double price, String category)
        {
            try
            {
                string query = "UPDATE products SET product_name='" + productName + "'," + "product_quantity='" + quantity + "'," + "product_price='" + price + "'," + "product_category='" + category + "'" + " WHERE product_id='" + productId + "'";
                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int total = cmd.ExecuteNonQuery();
                this.CloseConnection();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void userInsert(String userId, String userPassword, String userType)
        {
            string query = "INSERT INTO login (uid, password, user_type)" +
                "VALUES ('" + userId + "','" + userPassword + "','" + userType + "')";
            try
            {
                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to register.");
            }
        }
        public int removeUser(String userId)
        {
            try
            {
                string query = "DELETE FROM login WHERE uid='" + userId + "'";
                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int total = cmd.ExecuteNonQuery();
                this.CloseConnection();
                return total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
        public void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
    }
}