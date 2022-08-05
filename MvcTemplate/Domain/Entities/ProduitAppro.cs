using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProduitAppro")]
    public class ProduitAppro
    {
        [Key]
        public int ProduitAppro_Id { get; set; }
        [ForeignKey("Produit_Vendable")]
        public int ProduitAppro_ProduitID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProduitAppro_QuantiteProduite { get; set; }
        [ForeignKey("Forme_Produit")]
        public int? ProduitAppro_FormeProduitID { get; set; }
        [Column(TypeName = "int")]
        public int ProduitAppro_AbonnementID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProduitAppro_DateCreation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProduitAppro_DateModification { get; set; }
        [ForeignKey("Atelier")]
        public int ProduitAppro_AtelierID { get; set; }
        public Atelier Atelier { get; set; }
        public ProduitVendable Produit_Vendable { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
    }
}
