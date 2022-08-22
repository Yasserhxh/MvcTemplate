using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FactureModel
    {
     /*   public FactureModel()
        {
            listeBL = new List<BonDeLivraison_Model>();
        }*/
        public int Facture_ID { get; set; }
        public string Facture_Numero { get; set; }
        public int Facture_FournisseurID { get; set; }
        public int Facture_BonDeCommandeID { get; set; }
        public decimal Facture_MontantTVA { get; set; }
        public decimal Facture_TotalHT { get; set; }
        public decimal Facture_TotalTTC { get; set; }
        public int Facture_AbonnementID { get; set; }
       // public int Facture_PointStockID { get; set; }
        public DateTime Facture_DateFacture { get; set; }
        public DateTime Facture_DateSaisie { get; set; }
        public BonDeCommande_Model bonDeCommande { get; set; }
        public FournisseurModel Fournisseur { get; set; }
     //   public List<BonDeLivraison_Model> listeBL { get; set; }
       // public Lieu_StockageModel Lieu_Stockage { get; set; }
    }
}
