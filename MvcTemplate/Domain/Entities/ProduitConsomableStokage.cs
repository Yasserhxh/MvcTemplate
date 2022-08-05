using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProduitConsomable_Stokage")]
    public class ProduitConsomableStokage
    {
        [Key]
        public int ProduitConsomableStokage_Id { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int ProduitConsomableStokage_LieuStockID { get; set; }
        [ForeignKey("Produit_PretConsomer")]
        public int ProduitConsomableStokage_ProduitVendableId { get; set; }
        [ForeignKey("Forme_Produit")]
        public int ProduitConsomableStokage_FormeProduitId { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ProduitConsomableStokage_StockMinimum { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ProduitConsomableStokage_StockMaximum { get; set; }
        public int ProduitConsomableStokage_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int ProduitConsomableStokage_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProduitConsomableStokage_DateCreation { get; set; }
        public decimal ProduitConsomableStokage_QuantiteActuelle { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public ProduitPretConsomer Produit_PretConsomer { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
    }
}
