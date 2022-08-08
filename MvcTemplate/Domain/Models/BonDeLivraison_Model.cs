using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BonDeLivraison_Model
    {
        public int BonDeLivraison_ID { get; set; }
        public int BonDeLivraison_BCID { get; set; }
        public int BonDeLivraison_AbonnementID { get; set; }
        public decimal BonDeLivraison_TotalHT { get; set; }
        public decimal BonDeLivraison_TotalTVA { get; set; }
        public decimal BonDeLivraison_TotalTTC { get; set; }
        public BonDeCommande_Model Bon_De_Commande { get; set; }
    }
}
