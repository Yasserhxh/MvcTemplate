using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PdV_ProduitsPretModel
    {
        public int PdV_ProduitsPret_Id { get; set; }
        public int PdV_ProduitsPret_PointVenteId { get; set; }
        public int PdV_ProduitsPret_ProduitConsomableId { get; set; }
        public int PdV_ProduitsPret_FormeProduitId { get; set; }
        public decimal PdV_ProduitsPret_Quantite { get; set; }
        public DateTime PdV_ProduitsPret_DateModification { get; set; }
        public int PdV_ProduitsPret_AbonnementId { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
        public ProduitPretConsomerModel Produit_PretConsomer { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
    }
}
