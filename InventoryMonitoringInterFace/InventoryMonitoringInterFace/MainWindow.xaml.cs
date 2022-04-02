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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryMonitoringInterFace
{
    public partial class MainWindow : Window
    {
        LoginData ld = new LoginData();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            BLClass bl = new BLClass();
            String username = this.userId_textBox.Text;
            String password = this.password_passBox.Password;
            ld = bl.loginCheck(username, password);
            if(ld.errormsg == "" && ld.uType.Equals("manager"))
            {
                ManagerWindow1 mw1 = new ManagerWindow1();
                mw1.Show();
                this.Close();
            }
            else if(ld.errormsg == "" && ld.uType.Equals("sales"))
            {
                SalesManWindow sw = new SalesManWindow();
                sw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(ld.errormsg);
            }
        }
    }
}
