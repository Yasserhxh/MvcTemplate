using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Mouvement_Type")]
    public class MouvementType
    {
        [Key]
        public int MouvementType_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string MouvementType_Libelle { get; set; }
        [Column(TypeName = "int")]
        public int MouvementType_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MouvementType_DateCreation { get; set; }
    }
}
