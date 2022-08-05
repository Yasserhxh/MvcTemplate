using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("PackageForme_Details")]
    public class PackageForme_Details
    {
        [Key]
        public int PackageFormeDetails_ID { get; set; }
        [ForeignKey("Package_Forme")]
        public int PackageFormeDetails_PackageFormeID { get; set; }
        [ForeignKey("Sous_ProduitPackage")]
        public int PackageFormeDetails_SousProduitID { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal PackageFormeDetails_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PackageFormeDetails_CoutdeRevient { get; set; }
        public Package_Forme Package_Forme { get; set; }
        public Sous_ProduitPackage Sous_ProduitPackage { get; set; }
    }
}
