using Npgsql;
using NpgsqlTypes;
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
using static My_warmth.Connection; 

namespace My_warmth
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }



        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand cmd = GetCommand("Select \"email\", \"password\", bank_detalis, contract_number from \"Client\"" +
                "Where \"email\" = @em and \"password\" = @pass");
            cmd.Parameters.AddWithValue("@em", NpgsqlDbType.Varchar, TbEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, TbPassword.Text.Trim());
                NpgsqlDataReader result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                result.Close();
                NpgsqlCommand cmt = GetCommand("Select \"email\", \"password\", bank_detalis, contract_number from \"Client\", \"Individual_person\"" +
                    "where \"email\" = @em and \"password\" = @pass and \"Client\".email=\"Individual_person\".client");
                cmt.Parameters.AddWithValue("@em", NpgsqlDbType.Varchar, TbEmail.Text.Trim());
                cmt.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, TbPassword.Text.Trim());
                NpgsqlDataReader result1 = cmt.ExecuteReader();
                if (result1.HasRows)
                {

                    result1.Read();
                    Client client = new Client(result1.GetString(0), result1.GetString(1), result1.GetString(2), result1.GetInt64(3));
                    result1.Close();
                    PageControl.client = client;
                    PageControl.person = SelectTableIndividual(client.Email);
                    NavigationService.Navigate(PageControl.MainPage);
                }
                else
                {
                    result1.Close();
                    NpgsqlCommand cmd1 = GetCommand("Select \"email\", \"password\", bank_detalis, contract_number from \"Client\", \"Legal_person\"" +
                    "where \"email\" = @em and \"password\" = @pass and \"Client\".email=\"Legal_person\".client");
                    cmd1.Parameters.AddWithValue("@em", NpgsqlDbType.Varchar, TbEmail.Text.Trim());
                    cmd1.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, TbPassword.Text.Trim());
                    NpgsqlDataReader result2 = cmd1.ExecuteReader();
                    if(result2.HasRows)
                    {
                        result2.Read();
                        Client client = new Client(result2.GetString(0), result2.GetString(1), result2.GetString(2), result2.GetInt64(3));
                        result2.Close();
                        PageControl.client = client;
                        PageControl.lPerson = SelectTableLegal(client.Email);
                        NavigationService.Navigate(PageControl.mainLegal);
                    }
                }
                result1.Close();
                  

            }
            
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
            result.Close();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageControl.RegistrationPage);
        }
    }
}
