using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProduitPackage")]
    public class ProduitPackage
    {
        public ProduitPackage()
        {

            Sous_ProduitPackage = new Collection<Sous_ProduitPackage>();
            SousMatierePackages = new Collection<SousMatierePackage>();
            formes = new Collection<Forme_Produit>();
        }
        [Key]
        public int ProduitPackage_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string ProduitPackage_Photo { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int ProduitPackage_UniteVente { get; set; }
        [ForeignKey("Sous_Famille")]
        public int ProduitPackage_FamilleID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string ProduitPackage_Designation { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProduitPackage_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProduitPackage_CoutdeRevient { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProduitPackage_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int ProduitPackage_AbonnementID { get; set; }
        //[ForeignKey("Point_Vente")]
        //public int ProduitPackage_PointVenteID { get; set; }
        public ICollection<Sous_ProduitPackage> Sous_ProduitPackage { get; set; }
        public ICollection<SousMatierePackage> SousMatierePackages { get; set; }
        public ICollection<Forme_Produit> formes { get; set; }
        //public Point_Vente Point_Vente { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public SousFamille Sous_Famille { get; set; }

    }
}
