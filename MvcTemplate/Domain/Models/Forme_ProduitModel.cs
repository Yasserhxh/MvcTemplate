using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Forme_ProduitModel
    {
        
        public int FormeProduit_ID { get; set; }
        public int? FormeProduit_ProduitID { get; set; }
        public int? FormeProduit_ProduitPackageId { get; set; }
        public int? FormeProduit_ProduitPretId { get; set; }
        public string FormeProduit_Libelle { get; set; }
        public decimal FormeProduit_CoutDeRevient { get; set; }
        public decimal FormeProduit_PrixVente { get; set; }
        public DateTime FormeProduit_DateCreation { get; set; }
        public int FormeProduit_AbonnementID { get; set; }
        public ProduitPretConsomerModel Produit_PretConsomer { get; set; }
        public ProduitPackageModel ProduitPackage { get; set; }
        public ProduitVendableModel Produit_Vendable { get; set; }


    }
}
