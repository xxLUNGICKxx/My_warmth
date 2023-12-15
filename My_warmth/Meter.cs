using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_warmth
{
    public class Meter
    {
        public Meter(int id_meter, string model, DateTime date_issue, DateTime start_exploitation, int term_exploatation, DateTime muster, long contract_number)
        {
            IdMeter = id_meter;
            Model = model;
            DateIssue = date_issue;
            StartExploitation = start_exploitation;
            TermExploatation = term_exploatation;
            Muster = muster;
            Contract = contract_number;
        }
        public int IdMeter { get; set; }
        public string Model { get; set; }
        DateTime DateIssue { get; set; }
        DateTime StartExploitation { get; set; }
        int TermExploatation { get; set; }
        DateTime Muster { get; set; }
        long Contract { get; set; }

    }
}
