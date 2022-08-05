using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Taux_TVA")]
    public class Taux_TVA
    {
        [Key]
        public int TauxTVA_Id { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal TauxTVA_Pourcentage { get; set; }
        public int TauxTVA_AbonnementId { get; set; }
        public DateTime TauxTVA_DateCreation { get; set; }

    }
}
