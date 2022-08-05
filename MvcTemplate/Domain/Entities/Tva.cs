using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Tva")]
    public class Tva
    {
        [Key]
        public int tva_ID { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal tauxTva { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal totalHt { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal totalTtc { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal totalTva { get; set; }
        [ForeignKey("Commande")]
        public int? commande_ID { get; set; }
        [ForeignKey("Vente")]
        public int? vente_ID { get; set; }
    }
}
