using My_warmth;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
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

namespace My_warmth
{
    /// <summary>
    /// Логика взаимодействия для RegistrationLegal.xaml
    /// </summary>
    public partial class RegistrationLegal : Page
    {

        private Frame AppFrame;
        
        public RegistrationLegal(Frame AppFrame)
        {
            InitializeComponent();
            this.AppFrame = AppFrame;
        }
        private Client client1;
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var organization = TbOrganization.Text.Trim();
            long kpp = long.Parse(TbKpp.Text.Trim());
            long inn = long.Parse(TbInn.Text.Trim());
            var client = TbEmail.Text.Trim();
            var email = TbEmail.Text.Trim();
            var password = TbPassword.Text.Trim();
            var bank_detalis = TbBank.Text.Trim();
            long contract = long.Parse(TbContract.Text.Trim());

            NpgsqlCommand sql = Connection.GetCommand($"Select contract_number from \"Contract\" where contract_number = {contract}");
            NpgsqlDataReader reader = sql.ExecuteReader();
            if(reader.HasRows)
            {

                reader.Close();
                client1 = new Client(email, password, bank_detalis, contract);
                Connection.InsertTableClient(client1);
                Connection.InsertTableLegal(new LegalPerson(client, organization, kpp, inn));
                PageControl.client = client1;

                AppFrame.Navigate(PageControl.AuthorizationPage);
            }
            else
            {
                reader.Close();
                MessageBox.Show("Данного номера контракта не существует");
                
            }
            
            

        }

        private void TbKpp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox ctrl = sender as TextBox;
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            ctrl.MaxLength = 9;
        }

        private void TbInn_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox ctrl = sender as TextBox;
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            ctrl.MaxLength = 10;
        }

        private void TbContract_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox ctrl = sender as TextBox;
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            ctrl.MaxLength = 6;
        }
    }
}
