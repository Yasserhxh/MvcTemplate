using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Sous_ProduitPackage")]
    public class Sous_ProduitPackage
    {
        [Key]
        public int SousProduitPackage_ID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int SousProduitPackage_FormeProduittID { get; set; }
        [ForeignKey("ProduitPackage")]
        public int SousProduitPackage_ProduitPackageID { get; set; }
        //public int SousProduitPackage_ProduitID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SousProduitPackage_QuantiteProduit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SousProduitPackage_CoutDeRevient { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime SousProduitPackage_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int SousProduitPackage_AbonnementID { get; set; }
        public ProduitPackage ProduitPackage { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
    }
}
