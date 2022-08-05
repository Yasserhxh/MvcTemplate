using System;

namespace Domain.Models
{
    public class ProduitApproModel
    {
        public int ProduitAppro_Id { get; set; }
        public int ProduitAppro_ProduitID { get; set; }
        public int? ProduitAppro_FormeProduitID { get; set; }
        public decimal ProduitAppro_QuantiteProduite { get; set; }
        public int ProduitAppro_AbonnementID { get; set; }
        public DateTime ProduitAppro_DateCreation { get; set; }
        public DateTime ProduitAppro_DateModification { get; set; }
        public int ProduitAppro_AtelierID { get; set; }
        public ProduitVendableModel Produit_Vendable { get; set; }
        public AtelierModel Atelier { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }

    }
}
