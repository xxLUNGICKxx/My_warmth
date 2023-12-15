using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_warmth
{
    public class Contract
    {
        public Contract(long contract_number, DateTime start_date, DateTime end_date)
        {
            Contract_number = contract_number;
            Start_date = start_date;
            End_date = end_date;
            

        }
        public long Contract_number { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        
    }
}
