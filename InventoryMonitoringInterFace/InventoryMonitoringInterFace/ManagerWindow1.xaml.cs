using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryMonitoringInterFace
{
    public partial class ManagerWindow1 : Window
    {
        BLClass bl = new BLClass();
        DBHandler db = new DBHandler();
        public ManagerWindow1()
        {
            InitializeComponent();
        }
        
        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        public void loadDataGrid()
        {
            try
            {
                db.OpenConnection();
                String query = "SELECT * FROM products";
                MySqlCommand cmd = new MySqlCommand(query, db.connection);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable("Products");
                msda.Fill(dt);
                manager1_dataGridView.ItemsSource = dt.DefaultView;
                msda.Update(dt);
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void viewAll_btn_Click(object sender, RoutedEventArgs e)
        {
            this.loadDataGrid();
        }
        private void insert_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String productName = this.productName_textBox.Text;
                int quantity = int.Parse(quantity_textBox.Text);
                double price = double.Parse(price_textBox.Text);
                String category = this.category_combobox.Text;
                bl.insert(productName, quantity, price, category);
                this.productName_textBox.Text = "";
                this.quantity_textBox.Text = "";
                this.price_textBox.Text = "";
                this.category_combobox.Text = "";
                this.loadDataGrid();
                MessageBox.Show("Item added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert information correctly.");
            }
        }
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productId = int.Parse(productId_textBox.Text);
                int deleted = bl.delete(productId);
                
                if (deleted > 0)
                {
                    this.productId_textBox.Text = "";
                    this.loadDataGrid();
                    MessageBox.Show("Item deleted.");
                }
                else
                {
                    MessageBox.Show("Item not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select a product to delete.");
            }
        }
        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productId = int.Parse(productId_textBox.Text);
                String productName = this.productName_textBox.Text;
                int quantity = int.Parse(quantity_textBox.Text);
                double price = double.Parse(price_textBox.Text);
                String category = this.category_combobox.Text;
                int updated = bl.update(productId, productName, quantity, price, category);
                if(updated > 0)
                {
                    this.productId_textBox.Text = "";
                    this.productName_textBox.Text = "";
                    this.quantity_textBox.Text = "";
                    this.price_textBox.Text = "";
                    this.category_combobox.Text = "";
                    this.loadDataGrid();
                    MessageBox.Show("Inventory updated.");
                }
                else
                {
                    MessageBox.Show("Item not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select product and insert details correctly to update.");
            }
        }
        public void search()
        {
            try
            {
                String productName = this.productName_textBox.Text;
                if (productName.Equals(""))
                {
                    MessageBox.Show("Enter search keyword.");
                }
                else
                {
                    db.OpenConnection();
                    String query = "SELECT * FROM products where product_name like '%" + productName + "%'";
                    MySqlCommand cmd = new MySqlCommand(query, db.connection);
                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter msda = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    msda.Fill(dt);
                    manager1_dataGridView.ItemsSource = dt.DefaultView;
                    msda.Update(dt);
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item does not exist or Incorrect keyword.");
            }
        }
        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            this.search();
        }
        private void manager1_dataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected !=null)
            {
                productId_textBox.Text = row_selected[0].ToString();
            }
        }

        private void RegUser_btn_Click(object sender, RoutedEventArgs e)
        {
            NewUser nu = new NewUser();
            nu.Show();
            this.Close();
        }
    }
}
