using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace My_warmth
{
    public class Consumption
    {
        public Consumption(int number_documents, long contract_number, DateTime date, int quantity )
        {
            Document = number_documents;
            Contract = contract_number;
            Date = date;
            Quantity = quantity;
            Sum = Quantity * 2000;

        }
        public Consumption( long contract_number, DateTime date, int quantity)
        {
            
            Contract = contract_number;
            Date = date;
            Quantity = quantity;

        }
        public int Document { get; set; }
        public long Contract { get; set; }

        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public int Sum { get; set; }
        


    }
}
