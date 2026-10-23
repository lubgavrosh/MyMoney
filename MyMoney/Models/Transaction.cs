using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoney.Models
{
    public class Transaction
    {
        public string Opis { get; set; } = "";
        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }
        public bool CzyPrzychod { get; set; }
        public string Kategoria { get; set; } = "";

        // pole wyliczane, tylko do wyświetlania
        public string Typ => CzyPrzychod ? "Przychód" : "Wydatek";
    }
}
