using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PlanificationJourneeBase")]
    public class PlanificationJourneeBase
    {
        public PlanificationJourneeBase()
        {
            Planification_ProductionBase = new Collection<PlanificationdeProductionBase>();
        }
        [Key]
        public int PlanificationJourneeBase_ID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationJourneeBase_Date { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string PlanificationJourneeBase_Etat { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PlanificationJourneeBase_CoutDeRevient { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationJourneeBase_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int PlanificationJourneeBase_AbonnementID { get; set; }
        [ForeignKey("BonDe_Sortie")]
        public int PlanificationJourneeBase_BonDeSortieID { get; set; }
        [ForeignKey("Atelier")]
        public int? PlanificationJourneeBase_AtelierID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int? PlanificationJourneeBase_LieuStockageID { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string PlanificationJourneeBase_UtilisateurId { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string PlanificationJourneeBase_ValidePar { get; set; }
        [Column(TypeName = "bit")]
        public bool? PlanificationJourneeBase_SeenByStock { get; set; }
        [Column(TypeName = "bit")]
        public bool? PlanificationJourneeBase_SeenByAtelier { get; set; }
        public ICollection<PlanificationdeProductionBase> Planification_ProductionBase { get; set; }
        public BonDeSortie BonDe_Sortie { get; set; }
        public Atelier Atelier { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
    }
}
