using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Mode_Paiement")]
    public class ModePaiement
    {
        [Key]
        public int ModePaiement_Id { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        public string ModePaiement_Libelle { get; set; }
    }
}
