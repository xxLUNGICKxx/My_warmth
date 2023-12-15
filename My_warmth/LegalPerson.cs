using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace My_warmth
{
    public class LegalPerson
    {
        public LegalPerson(string client, string organization, long kpp, long inn)
        {
            Client = client;
            Organization = organization;
            Kpp = kpp;
            Inn = inn;
        }
        public string Client { get; set; }
        public string Organization { get; set; }
        public long Kpp { get; set; }
        public long Inn { get; set; }

    }
}
