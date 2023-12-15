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
using Npgsql;

namespace My_warmth
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow() 
        {
            InitializeComponent();
            AppFrame.Navigate(new Authorization());
            DataContext = this;
            string connectionString = "host=localhost;database=Diplom;port=5432;username=postgres;password=123";
            Connection.Connect(connectionString);   
            PageControl.SetAppFrame(AppFrame);

        }
    }
}
