using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Bon_De_Livraison")]
    public class BonDeLivraison
    {
        public BonDeLivraison()
        {

            listeArticles = new Collection<Article_BL>();
        }
        [Key]
        public int BonDeLivraison_ID { get; set; }
        [ForeignKey("Bon_De_Commande")]
        public int BonDeLivraison_BCID { get; set; }
        public string BonDeLivraison_Designation { get; set; }
        public int BonDeLivraison_AbonnementID { get; set; }
        [ForeignKey("Facture")]
        public int? BonDeLivraison_FactureID { get; set; }
        [ForeignKey("Statut_BL")]
        public int? BonDeLivraison_StatutID { get; set; }
        public decimal BonDeLivraison_TotalHT { get; set; }
        public decimal BonDeLivraison_TotalTVA { get; set; }
        public decimal BonDeLivraison_TotalTTC { get; set; }
        public DateTime BonDeLivraison_DateLivraison { get; set; }
        public DateTime BonDeLivraison_DateSaisie { get; set; }
        public BonDeCommande Bon_De_Commande { get; set; }   
        public ICollection<Article_BL> listeArticles { get; set; }
        public Facture Facture { get; set; }
        public Statut_BL Statut_BL { get; set; }

    }
}
