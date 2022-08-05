using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProduitPack_AtelierModel
    {
        public int ProduitPackAtelier_ID { get; set; }
        public int ProduitPackAtelier_AtelierID { get; set; }
        public int ProduitPackAtelier_ProduitPackID { get; set; }
        public int ProduitPackAtelier_FormeID { get; set; }
        public decimal ProduitPackAtelier_Quantite { get; set; }
        public DateTime ProduitPackAtelier_DateCreation { get; set; }
        public int ProduitPackAtelier_AbonnementID { get; set; }
        public AtelierModel Atelier { get; set; }
        public ProduitPackageModel ProduitPackage { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
    }
}
