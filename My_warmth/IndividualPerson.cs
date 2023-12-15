using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_warmth
{
    public class IndividualPerson
    {
        public IndividualPerson(string client, string first_name, string last_name, string patronymic)
            {
                Client = client;
                First_name = first_name;
                Last_name = last_name;
                Patronymic = patronymic;
            
            }
        public  string Client { get; set; }
        public string First_name { get; set; }
        public  string Last_name { get; set; }
        public  string Patronymic { get; set; }




    }
}
