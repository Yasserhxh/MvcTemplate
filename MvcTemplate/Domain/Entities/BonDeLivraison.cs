using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Bon_De_Livraison")]
    public class BonDeLivraison
    {
        [Key]
        public int BonDeLivraison_ID { get; set; }
        [ForeignKey("Bon_De_Commande")]
        public int BonDeLivraison_BCID { get; set; }
        public int BonDeLivraison_AbonnementID { get; set; }
        public decimal BonDeLivraison_TotalHT { get; set; }
        public decimal BonDeLivraison_TotalTVA { get; set; }
        public decimal BonDeLivraison_TotalTTC { get; set; }
        public BonDeCommande Bon_De_Commande { get; set; }    

    }
}
