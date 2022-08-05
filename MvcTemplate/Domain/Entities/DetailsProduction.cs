using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Production_Details")]
    public class DetailsProduction
    {
        [Key]
        public int ProductionDetails_Id { get; set; }
        [ForeignKey("Package_Production")]
        public int ProductionDetails_ProductionID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int ProductionDetails_FormeID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductionDetails_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductionDetails_CoutDeRevient { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public PackageProduction Package_Production { get; set; }
    }
}
