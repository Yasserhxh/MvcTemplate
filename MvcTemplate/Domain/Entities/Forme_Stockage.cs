using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Forme_Stockage")]
    public class Forme_Stockage
    {
        [Key]
        public int FormStockage_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string FormStockage_Libelle { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FormStockage_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int FormStockage_AbonnementId { get; set; }

    }

}
