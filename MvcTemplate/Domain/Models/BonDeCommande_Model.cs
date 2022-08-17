using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BonDeCommande_Model
    {
        public BonDeCommande_Model()
        {
            listeArticles = new List<ArticleBC_Model>();
        }
        public int BonDeCommande_ID { get; set; }
        public string BonDeCommande_Numero { get; set; }
        public int BonDeCommande_FournisseurID { get; set; }
        public string BonDeCommande_CreePar { get; set; }
        public int BonDeCommande_PointStockID { get; set; }
        public string BonDeCommande_ValidéPar { get; set; }
        public DateTime BonDeCommande_DateCreation { get; set; }
        public DateTime? BonDeCommande_DateValidation { get; set; }
        public int BonDeCommande_AbonnementID { get; set; }
        public decimal BonDeCommande_TotalHT { get; set; }
        public decimal BonDeCommande_TotalTVA { get; set; }
        public decimal BonDeCommande_TotalTTC { get; set; }
        public string BonDeCommande_Statut { get; set; }
        public FournisseurModel Fournisseur { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public List<ArticleBC_Model> listeArticles { get; set; }
        public Abonnement_ClientModel Abonnement_Client { get; set; }


    }
}
