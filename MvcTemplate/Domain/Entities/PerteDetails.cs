using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Perte_Details")]
    public class PerteDetails
    {
        [Key]
        public int PerteDetails_ID { get; set; }
        [ForeignKey("Perte")]
        public int PerteDetails_PerteID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int PerteDetails_FormeID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int PerteDetails_UniteVenteID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PerteDetails_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PerteDetails_CoutDeRevient { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PerteDetails_DatePerte { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Perte Perte { get; set; }

    }
}
