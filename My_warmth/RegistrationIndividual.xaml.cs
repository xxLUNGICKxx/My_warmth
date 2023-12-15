using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace My_warmth
{
    /// <summary>
    /// Логика взаимодействия для RegistrationIndividual.xaml
    /// </summary>
    public partial class RegistrationIndividual : Page
    {
        private Frame AppFrame;
        public RegistrationIndividual(Frame AppFrame)
        {
            InitializeComponent();
            this.AppFrame = AppFrame;

        }
        private Client client1;
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            var first_name = TbFirstName.Text.Trim();
            var last_name = TbLastName.Text.Trim();
            var patronymic = TbPatronymic.Text.Trim();
            var client = TbEmail.Text.Trim();
            var email = TbEmail.Text.Trim();
            var password = TbPassword.Text.Trim();
            var bank_detalis = TbBank.Text.Trim();
            long contract = long.Parse(TbNumberContract.Text.Trim());

            NpgsqlCommand sql = Connection.GetCommand($"Select contract_number from \"Contract\" where contract_number = {contract}");
            NpgsqlDataReader reader = sql.ExecuteReader();

            if (reader.HasRows)
            {

                reader.Close();
                client1 = new Client(email, password, bank_detalis, contract);
                Connection.InsertTableClient(client1);
                Connection.InsertTableIndividual(new IndividualPerson(client, first_name, last_name, patronymic));
                PageControl.client = client1;
                
                AppFrame.Navigate(PageControl.AuthorizationPage);
                MessageBox.Show("Вы успешно зарегистрировались!");
            }
            else
            {
                
                reader.Close();
                MessageBox.Show("Данного номера контракта не существует");
            }

        }

        private void TbNumberContract_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox ctrl = sender as TextBox;
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
            ctrl.MaxLength = 6;
        }
    }
}
