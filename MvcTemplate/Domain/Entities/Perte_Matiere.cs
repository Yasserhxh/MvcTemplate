using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Perte_Matiere")]
    public class Perte_Matiere
    {
        [Key]
        public int PerteMatiere_ID { get; set; }
        [ForeignKey("matiereStock_Atelier")]
        public int PerteMatiere_MatierePremiereStockageID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PerteMatiere_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int PerteMatiere_UniteMesureID { get; set; }
        [ForeignKey("Atelier")]
        public int PerteMatiere_AtelierID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PerteMatiere_DateCreation { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public int PerteMatiere_Utilisateur { get; set; }
        [Column(TypeName = "int")]
        public int PerteMatiere_AbonnementID { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public MatiereStock_Atelier matiereStock_Atelier { get; set; }
        public Atelier Atelier { get; set; }
    }
}
