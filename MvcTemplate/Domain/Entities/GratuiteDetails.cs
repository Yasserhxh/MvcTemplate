using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("GratuiteDetails")]
    public class GratuiteDetails
    {
        [Key]
        public int GratuiteDetails_ID { get; set; }
        [ForeignKey("Gratuite")]
        public int GratuiteDetails_GratuiteID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int GratuiteDetails_FormeID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int GratuiteDetails_UniteVenteID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal GratuiteDetails_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal GratuiteDetails_CoutDeRevient { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime GratuiteDetails_DateCreation { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Gratuite Gratuite { get; set; }
    }
}
