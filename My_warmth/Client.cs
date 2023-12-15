using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_warmth
{
    public class Client
    {
        public Client(string email, string password, string bank_detalis, long contract_number)
        {
            Email = email;
            Password = password;
            Bank_detalis = bank_detalis;
            Contract_number = contract_number;

        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bank_detalis { get; set; }
        public long Contract_number { get; set; }

        
    }
}
