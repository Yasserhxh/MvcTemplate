using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Matiere_Famille")]
    public class MatiereFamille
    {
        [Key]
        public int MatiereFamille_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string MatiereFamille_Libelle { get; set; }
        [ForeignKey("MatiereFamille_Parent")]
        public int MatiereFamille_ParentID { get; set; }
        [Column(TypeName = "int")]
        public int MatiereFamille_AbonnementID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MatiereFamille_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int MatiereFamille_IsActive { get; set; }
        public MatireFamille_Parent MatiereFamille_Parent { get; set; }
    }
}
