using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Package_Forme")]
    public class Package_Forme
    {
        public Package_Forme()
        {
            details = new Collection<PackageForme_Details>();
            detailsMatiere = new Collection<PackageFormeDetails_Matiere>();
        }
        [Key]
        public int PackageForme_ID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int PackageForme_FormeProduitID { get; set; }
        [ForeignKey("ProduitPackage")]
        public int PackageForme_ProduitPackageID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PackageForme_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int PackageForme_AbonnementID { get; set; } 
        [Column(TypeName = "bit")]
        public bool PackageForme_IsInUse { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public ProduitPackage ProduitPackage { get; set; }
        public ICollection<PackageForme_Details> details { get; set; }
        public ICollection<PackageFormeDetails_Matiere> detailsMatiere { get; set; }
    }
}
