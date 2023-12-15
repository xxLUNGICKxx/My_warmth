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
    /// Логика взаимодействия для HomePageLegal.xaml
    /// </summary>
    public partial class HomePageLegal : Page
    {
        public HomePageLegal(Client client, LegalPerson homePageLegal)
        {
            InitializeComponent();
            BindingClient(client, homePageLegal);
            BindingMeter(client.Email);
            BindingConsumption(client.Email);
        }
        public void BindingClient(Client client, LegalPerson homePageLegal)
        {
            lEmail.Content = client.Email;
            ldetalis.Content = client.Bank_detalis;
            lContr.Content = client.Contract_number;

            lOrg.Content = homePageLegal.Organization;
            lkpp.Content = homePageLegal.Kpp;
            lInn.Content = homePageLegal.Inn;
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
            Readings.Visibility= Visibility.Hidden;
            SelectTableConsumption(PageControl.client.Email);
        }

        private void buttonReadings_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Hidden;
            Reckoning.Visibility = Visibility.Hidden;
            Readings.Visibility = Visibility.Visible;
        }

        private void ReadingsButton_Click(object sender, RoutedEventArgs e)
        {
            int quantity = int.Parse(tbReadings.Text.Trim());
            var meter = Meter.Text.Trim();
            InsertTableConsumption(new Consumption(PageControl.client.Contract_number, DateTime.Now, quantity), PageControl.client.Email);
            tbReadings.Clear();
        }
    }
    
}

