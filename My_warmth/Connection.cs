using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace My_warmth
{
    public class Connection
        
    {
        public static NpgsqlConnection connection;
        public static ObservableCollection<Client> clients { get; set; } = new ObservableCollection<Client>();
        public static ObservableCollection<IndividualPerson> individuals { get; set; } = new ObservableCollection<IndividualPerson>();
        public static ObservableCollection<Meter> meters { get; set; } = new ObservableCollection<Meter>();
        public static ObservableCollection<Consumption> consumptions { get; set; } = new ObservableCollection<Consumption>();
        public static void Connect(string connectionString)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            
        }
        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = sql;
            return command;
        }

        public static void InsertTableIndividual(IndividualPerson individualPerson)
        {
            NpgsqlCommand cmd = GetCommand("Insert into \"Individual_person\" (client, first_name, last_name, patronymic)" +
                " values (@client,@firstN, @lastN, @patr)");
            cmd.Parameters.AddWithValue("@client", NpgsqlDbType.Varchar, individualPerson.Client);
            cmd.Parameters.AddWithValue("@firstN", NpgsqlDbType.Varchar, individualPerson.First_name);
            cmd.Parameters.AddWithValue("@lastN", NpgsqlDbType.Varchar, individualPerson.Last_name);
            cmd.Parameters.AddWithValue("@patr", NpgsqlDbType.Varchar, individualPerson.Patronymic);
            cmd.ExecuteNonQuery();
            
        }

        public static void InsertTableClient(Client client)
        {
            NpgsqlCommand cmd = GetCommand("Insert into \"Client\" (email, password, bank_detalis, contract_number)" +
                " values (@email, @pass, @bank, @contract)");
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Varchar, client.Email);
            cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, client.Password);
            cmd.Parameters.AddWithValue("@bank", NpgsqlDbType.Varchar, client.Bank_detalis);
            cmd.Parameters.AddWithValue("@contract", NpgsqlDbType.Bigint, client.Contract_number);

            var result= cmd.ExecuteNonQuery();
        }

        public static void InsertTableLegal(LegalPerson legalPerson)
        {
            NpgsqlCommand cmd = GetCommand("Insert into \"Legal_person\" (client, organization, \"KPP\", \"INN\")" +
                " values (@client, @organ, @kpp, @inn)");
            cmd.Parameters.AddWithValue("@client", NpgsqlDbType.Varchar, legalPerson.Client);
            cmd.Parameters.AddWithValue("@organ", NpgsqlDbType.Varchar, legalPerson.Organization);
            cmd.Parameters.AddWithValue("@kpp", NpgsqlDbType.Bigint, legalPerson.Kpp);
            cmd.Parameters.AddWithValue("@inn", NpgsqlDbType.Bigint, legalPerson.Inn);
            cmd.ExecuteNonQuery();
        }

        public static IndividualPerson SelectTableIndividual(string client)
        {
            
            NpgsqlCommand cmd = GetCommand("select client, first_name, last_name, patronymic  from \"Individual_person\", \"Client\" " +
                $"where \"Individual_person\".client = \"Client\".email and \"Individual_person\".client = '{client}'");
            NpgsqlDataReader result = cmd.ExecuteReader();
            if (result.HasRows)
            {
                
                result.Read();
                    
                    var  person = new IndividualPerson(result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3));

                
                result.Close();
                return person;
            }
            else
            {
                result.Close();
                return null;
            }
        }

        public static LegalPerson SelectTableLegal(string client)
        {
            NpgsqlCommand cmd = GetCommand("select client, organization, \"KPP\", \"INN\" from \"Legal_person\", \"Client\"" +
                $"where \"Legal_person\".client = \"Client\".email and \"Legal_person\".client = '{client}'");
            NpgsqlDataReader result = cmd.ExecuteReader();
            if(result.HasRows)
            {
                result.Read();
                var person = new LegalPerson(result.GetString(0), result.GetString(1), result.GetInt64(2), result.GetInt64(3));
                result.Close();
                return person;
            }
            else
            {
                result.Close();
                return null;
            }
        }
        public static void InsertTableConsumption(Consumption consumption, string client)
        {
            NpgsqlCommand cmd = GetCommand("Insert into \"Consumption\" (contract_number, date, quantity)" +
                "values (@contract, @date, @quan)");
            
            cmd.Parameters.AddWithValue("@contract", NpgsqlDbType.Bigint, consumption.Contract);
            cmd.Parameters.AddWithValue("@date", NpgsqlDbType.Date, consumption.Date = DateTime.Now);
            cmd.Parameters.AddWithValue("@quan", NpgsqlDbType.Bigint, consumption.Quantity);
            cmd.ExecuteNonQuery();

        }
        public static void SelectTableMeter(string client)
        {
            NpgsqlCommand cmd = GetCommand("Select * from \"Meter\", \"Contract\", \"Client\" " +
                $"Where \"Meter\".contract_number = \"Contract\".contract_number and \"Contract\".contract_number = \"Client\".contract_number and \"Client\".email ='{client}' ");
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                    meters.Add(new Meter(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetInt32(4), reader.GetDateTime(5), reader.GetInt64(6)));
                reader.Close();
            }
        }

        public static void SelectTableConsumption(string client)
        {
            consumptions.Clear();
            NpgsqlCommand cmd = GetCommand("Select * from \"Consumption\", \"Contract\", \"Client\"" +
                $"Where \"Client\".contract_number = \"Contract\".contract_number and \"Contract\".contract_number = \"Consumption\".contract_number and \"Client\".email = '{client}'");
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                    consumptions.Add(new Consumption(reader.GetInt32(0), reader.GetInt64(1), reader.GetDateTime(2), reader.GetInt32(3)));
                

            }
            reader.Close();
        }

    }
}
