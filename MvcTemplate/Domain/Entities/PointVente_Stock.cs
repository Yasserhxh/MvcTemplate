using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PointVente_Stock")]
    public class PointVente_Stock
    {
        [Key]
        public int PointVenteStock_Id { get; set; }
        [ForeignKey("Produit_Vendable")]
        public int PointVenteStock_ProduitID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int PointVenteStock_FormeProduitID { get; set; }
        [ForeignKey("Point_Vente")]
        public int PointVenteStock_PointVenteID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PointVenteStock_QuantiteProduit { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PointVenteStock_DateModification { get; set; }
        [Column(TypeName = "int")]
        public int PointVenteStock_AbonnementID { get; set; }

        public Point_Vente Point_Vente { get; set; }
        public ProduitVendable Produit_Vendable { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
    }
}
