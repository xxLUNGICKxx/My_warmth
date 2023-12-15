using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Net.Mail;
using System.Net;


namespace My_warmth
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        public HomePage(Client client, IndividualPerson individualPerson)
        {
            InitializeComponent();
            BindingClient(client, individualPerson);
            BindingMeter(client.Email);
            BindingConsumption(client.Email);
            //SendMail(client.Email);
        }
        public void BindingClient(Client client, IndividualPerson individualPerson)
        {
            lEmail.Content = client.Email;
            ldetalis.Content = client.Bank_detalis;
            lContr.Content = client.Contract_number;

            lFName.Content = individualPerson.First_name;
            lLName.Content = individualPerson.Last_name;
            lPatr.Content = individualPerson.Patronymic;


        }
        private void BindingMeter(string client)
        {
            Binding binding = new Binding();
            Connection.SelectTableMeter(client);
            binding.Source = Connection.meters;
            Meter.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            
        }
        private void BindingConsumption(string client)
        {
            Binding binding = new Binding();
            Connection.SelectTableConsumption(client);
            binding.Source = Connection.consumptions;
            lConsumption.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }
        private void ButtonProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Visible;
            Reckoning.Visibility = Visibility.Hidden;
            Readings.Visibility = Visibility.Hidden;


        }



        private void BottonReckoning_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Hidden;
            Reckoning.Visibility = Visibility.Visible;
            Readings.Visibility = Visibility.Hidden;

            SelectTableConsumption(PageControl.client.Email);
        }
        

        private void readingsButton_Click(object sender, RoutedEventArgs e)
        {
            
            

            Profile.Visibility = Visibility.Hidden;
            Reckoning.Visibility = Visibility.Hidden;
            Readings.Visibility = Visibility.Visible;
        }

        //private void SendMail(string client )
        //{
        //    var file = lConsumption.SelectedItem as Consumption;

        //    MailAddress from = new MailAddress("nikitaignatenko88@mail.ru", "ООО Теплолюкс");
        //    MailAddress to = new MailAddress("nikitaignatenko88@mail.ru");
        //    MailMessage m = new MailMessage(from, to);
        //    m.Subject = "Тест";
        //    m.Body = "За этот месяц вы должны заплатиь";
        //    m.IsBodyHtml = true;
        //    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 465);
        //    smtp.Credentials = new NetworkCredential("nikitaignatenko88@mail.ru", "H18umNhgXTiSute7MEw7");
        //    smtp.EnableSsl = true;
        //    smtp.Send(m);
        //    MessageBox.Show("Письмо отправленно");
        //}

        private void buttonReadings_Click(object sender, RoutedEventArgs e)
        {

            
            int quantity = int.Parse(tbReadings.Text.Trim());
            var meter = Meter.Text.Trim();
            InsertTableConsumption(new Consumption(PageControl.client.Contract_number, DateTime.Now, quantity), PageControl.client.Email);
            tbReadings.Clear();
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {

            MailAddress from = new MailAddress("nikitaignatenko88@mail.ru", "ООО Теплолюкс");
            MailAddress to = new MailAddress(PageControl.client.Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = "В будущем здесь должна быть оплата";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("nikitaignatenko88@mail.ru", "H18umNhgXTiSute7MEw7");
            smtp.EnableSsl = true;
            smtp.Send(m);
            MessageBox.Show("Письмо отправленно");
        }
    }
}
