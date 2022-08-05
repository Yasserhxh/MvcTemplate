using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Perte_MatiereStock")]
    public class Perte_MatiereStock
    {
        [Key]
        public int PerteMatiereStock_ID { get; set; }
        [ForeignKey("MatierePremiere_Stokage")]
        public int PerteMatiere_MatierePremiereStockageID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PerteMatiere_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int PerteMatiere_UniteMesureID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int PerteMatiere_StockID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PerteMatiere_DateCreation { get; set; }
        [ForeignKey("User")]
        public string PerteMatiere_Utilisateur { get; set; }
        [Column(TypeName = "int")]
        public int PerteMatiere_AbonnementID { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public MatierePremiereStockage MatierePremiere_Stokage { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public ApplicationUser User { get; set; }
    }
}
