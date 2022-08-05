using System;

namespace Domain.Models
{
    public class Sous_ProduitPackageModel
    {
        public int SousProduitPackage_ID { get; set; }
        //public int SousProduitPackage_ProduitID { get; set; }
        public int SousProduitPackage_FormeProduittID { get; set; }
        public int SousProduitPackage_ProduitPackageID { get; set; }
        public decimal SousProduitPackage_QuantiteProduit { get; set; }
        public decimal SousProduitPackage_CoutDeRevient { get; set; }
        public DateTime SousProduitPackage_DateCreation { get; set; }
        public int SousProduitPackage_AbonnementID { get; set; }
        public ProduitPackageModel ProduitPackage { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }


    }
}
