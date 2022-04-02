using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        BLClass bl = new BLClass();
        public NewUser()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow1 mw1 = new ManagerWindow1();
            mw1.Show();
            this.Close();
        }

        private void userReg_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String userId = this.userId_textBox.Text;
                String userPassword = this.userPass_textBox.Password;
                String userType = this.userType_textBox.Text;
                bl.registerUser(userId, userPassword, userType);
                this.userId_textBox.Text = "";
                this.userPass_textBox.Password = "";
                this.userType_textBox.Text = "";
                MessageBox.Show("User Registered!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert information correctly.");
            }
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String userId = userId_textBox.Text;
                int deleted = bl.deleteUser(userId);

                if (deleted > 0)
                {
                    this.userId_textBox.Text = "";
                    MessageBox.Show("User Removed.");
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert correct ID to delete item.");
            }
        }
    }
}
