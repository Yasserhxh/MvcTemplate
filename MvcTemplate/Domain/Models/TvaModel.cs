using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TvaModel
    {
        public int tva_ID { get; set; }
        public decimal tauxTva { get; set; }
        public decimal totalHt { get; set; }
        public decimal totalTtc { get; set; }
        public decimal totalTva { get; set; }
        public int? commande_ID { get; set; }
        public int? vente_ID { get; set; }
    }
}
