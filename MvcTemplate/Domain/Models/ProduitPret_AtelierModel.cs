using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProduitPret_AtelierModel
    {
        public int ProduitPretAtelier_ID { get; set; }
        public int ProduitPretAtelier_AtelierID { get; set; }
        public int ProduitPretAtelier_FormeID { get; set; }
        public int ProduitPretAtelier__ProduitID { get; set; }
        public decimal ProduitPretAtelier_Quantite { get; set; }
        public DateTime ProduitPretAtelier_DateCreation { get; set; }
        public AtelierModel Atelier { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public ProduitPretConsomerModel Produit_PretConsomer { get; set; }
    }
}
