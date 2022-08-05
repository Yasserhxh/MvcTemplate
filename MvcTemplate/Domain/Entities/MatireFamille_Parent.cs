using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("MatiereFamille_Parent")]
    public class MatireFamille_Parent
    {
        [Key]
        public int MatiereFamilleParent_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string MatiereFamilleParent_Libelle { get; set; }
        [Column(TypeName = "int")]
        public int MatiereFamilleParent_AbonnementID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MatiereFamilleParent_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int MatiereFamilleParent_IsActive { get; set; }
    }
}
