using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PaiementComandeApi
    {
        public string userID { get; set; }
        public int commandeID { get; set; }
        public bool flag { get; set; }
        public decimal? montant { get; set; }
        public int? modePaiement { get; set; }
    }
}
