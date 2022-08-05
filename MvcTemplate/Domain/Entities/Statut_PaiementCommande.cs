using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Statut_PaiementCommande")]
    public class Statut_PaiementCommande
    {
        [Key]
        public int StatutPaiementCommande_ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string StatutPaiementCommande_Libelle { get; set; }
    }
}
