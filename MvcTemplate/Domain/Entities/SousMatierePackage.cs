using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SousMatierePackage")]
    public class SousMatierePackage
    {
        [Key]
        public int SousMatierePackage_ID { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int SousMatierePackage_MatiereID { get; set; }
        [ForeignKey("ProduitPackage")]
        public int SousMatierePackage_ProduitPackageID { get; set; }
        [Column(TypeName = "int")]
        public int SousMatierePackage_AbonnementID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime SousMatierePackage_DateCreation { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
        public ProduitPackage ProduitPackage { get; set; }
    }
}
