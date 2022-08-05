using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("PdV_ProduitsPret")]
    public class PdV_ProduitsPret
    {
        [Key]
        public int PdV_ProduitsPret_Id { get; set; }
        [ForeignKey("Point_Vente")]
        public int PdV_ProduitsPret_PointVenteId { get; set; }
        [ForeignKey("Produit_PretConsomer")]
        public int PdV_ProduitsPret_ProduitConsomableId { get; set; }
        [ForeignKey("Forme_Produit")]
        public int PdV_ProduitsPret_FormeProduitId { get; set; }
        public decimal PdV_ProduitsPret_Quantite { get; set; }
        public DateTime PdV_ProduitsPret_DateModification { get; set; }
        public int PdV_ProduitsPret_AbonnementId { get; set; }
        public Point_Vente Point_Vente { get; set; }
        public ProduitPretConsomer Produit_PretConsomer { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
    }
}
