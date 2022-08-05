using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Bon_Details")]
    public class BonDetails
    {
        [Key]
        public int BonDetails_ID { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal BonDeSortie_Quantite { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal BonDeSortie_QuantiteEnStock { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal BonDeSortie_QuantiteDemandee { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal BonDeSortie_QuantiteLivree { get; set; } 
       
        [ForeignKey("Unite_Mesure")]
        public int BonDeSortie_UniteMesureId { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int BonDeSortie_MatiereId { get; set; }
        [ForeignKey("BonDe_Sortie")]
        public int BonDeSortie_BonDeSortieID { get; set; }
        [NotMapped]
        public string BonDeSortie_MatiereLibelle { get; set; }
        [NotMapped]
        public string BonDeSortie_UniteLibelle { get; set; }
        [NotMapped]
        public string BonDeSorite_UniteStock { get; set; }
        [NotMapped]
        public decimal BonDeSortie_QuantiteEnStockAvecPlan { get; set; }


        public BonDeSortie BonDe_Sortie { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
    }
}
