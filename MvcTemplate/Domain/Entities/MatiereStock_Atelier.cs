using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("matiereStock_Atelier")]
    public class MatiereStock_Atelier
    {
        [Key]
        public int MatiereStockAtelier_ID { get; set; }
        [ForeignKey("Atelier")]
        public int MatiereStockAtelier_AtelierID { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int MatiereStockAtelier_MatierePremiereID { get; set; }
        [Column(TypeName ="decimal(18,3)")]
        public decimal MatiereStockAtelier_QauntiteActuelle { get; set; }
        [Column(TypeName ="decimal(18,3)")]
        public decimal MatiereStockAtelier_QuatiteAvecPlanification { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MatiereStockAtelier_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int MatiereStockAtelier_AbonnementID { get; set; }
        public Atelier Atelier { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
    }
}
