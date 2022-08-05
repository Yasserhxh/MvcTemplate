using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Package_Production")]
    public class PackageProduction
    {
        //public PackageProduction()
        //{
            //detailsProduction = new Collection<DetailsProduction>();
        //}
        [Key]
        public int PackageProduction_ID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int PackageProduction_ProduitPackageID { get; set; }
        public DateTime PackageProduction_DateCreation { get; set; }
        [ForeignKey("ProduitPackage")]
        public int PackageProduction_ProduitID { get; set; }
        public int PackageProduction_AbonnementID { get; set; }
        public string PackageProduction_UtilisateurID { get; set; }
        public decimal PackageProduction_Quantite { get; set; }
        //public ICollection<DetailsProduction> detailsProduction { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public ProduitPackage ProduitPackage { get; set; }
    }
}
