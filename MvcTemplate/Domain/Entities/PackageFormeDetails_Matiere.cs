using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("PackageFormeDetailsMatiere")]
    public class PackageFormeDetails_Matiere
    {
        [Key]
        public int PackageFormeDetailsMatiere_ID { get; set; }
        [ForeignKey("Package_Forme")]
        public int PackageFormeDetailsMatiere_PackageFormeID { get; set; }
        [ForeignKey("SousMatierePackage")]
        public int PackageFormeDetailsMatiere_SousMatiereID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int PackageFormeDetailsMatiere_UniteMesureID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PackageFormeDetailsMatiere_Quantite { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PackageFormeDetailsMatiere_CoutDeRevient { get; set; }
        public Package_Forme Package_Forme { get; set; }
        public SousMatierePackage SousMatierePackage { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
