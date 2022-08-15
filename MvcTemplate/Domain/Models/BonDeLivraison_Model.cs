using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BonDeLivraison_Model
    {
        public BonDeLivraison_Model()
        {
            listeArticles = new List<ArticleBL_Model>();
        }
        public int BonDeLivraison_ID { get; set; }
        public int BonDeLivraison_BCID { get; set; }
        public string BonDeLivraison_Designation { get; set; }
        public int BonDeLivraison_AbonnementID { get; set; }
        public int? BonDeLivraison_FactureID { get; set; }
        public int? BonDeLivraison_StatutID { get; set; }
        public decimal BonDeLivraison_TotalHT { get; set; }
        public decimal BonDeLivraison_TotalTVA { get; set; }
        public decimal BonDeLivraison_TotalTTC { get; set; }
        public DateTime BonDeLivraison_DateLivraison { get; set; }
        public DateTime BonDeLivraison_DateSaisie { get; set; }
        public BonDeCommande_Model Bon_De_Commande { get; set; }
        public ICollection<ArticleBL_Model> listeArticles { get; set; }
        public FactureModel Facture { get; set; }
        public Statut_BLModel Statut_BL { get; set; }

    }

}
