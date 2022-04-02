using System;
using System.Collections;
using System.Text;
using System.Windows;

namespace InventoryMonitoringInterFace
{
    public class BLClass : MainWindow
    {
        DBHandler db = new DBHandler();
        MainWindow mw = new MainWindow();
        LoginData ld = new LoginData();
        Products p = new Products();
        public LoginData loginCheck(String username, String password)
        {
            if (username.Equals("") || password.Equals("") || password.Length < 8)
            {
                ld.errormsg = "Either username or password is empty or password is too short";
            }
            else
            {
                ld = db.getLoginInfo(username, password);
            }
            return ld;
        }
        public ArrayList searchProducts(String productName)
        {
            return db.search(productName);
        }
        public void insert(String productName, int quantity, double price, String category)
        {
            db.insertData(productName, quantity, price, category);
        }
        public int delete(int productId)
        {
            return db.deleteData(productId);
        }
        public int update(int productId, String productName, int quantity, double price, String category)
        {
            return db.updateData(productId, productName, quantity, price, category);
        }
        public void registerUser(String userId, String userPassword, String userType)
        {
            db.userInsert(userId, userPassword, userType);
        }
        public int deleteUser(String userId)
        {
            return db.removeUser(userId);
        }
    }
}