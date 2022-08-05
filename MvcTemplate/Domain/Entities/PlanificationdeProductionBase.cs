using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("PlanificationdeProductionBase")]
    public class PlanificationdeProductionBase
    {
        [Key]
        public int PlanificationProductionBase_Id { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationProductionBase_Date { get; set; }
        [ForeignKey("ProduitBase")]
        public int PlanificationProductionBase_ProduitBase { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PlanificationProductionBase_QuantitePrevue { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PlanificationProductionBase_QuantiteProduite { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PlanificationProductionBase_CoutRevient { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PlanificationProductionBase_QuantiteRestante { get; set; }
        [Column(TypeName = "int")]
        public int PlanificationProductionBase_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationProductionBase_DateCreation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanificationProductionBase_DateModification { get; set; }
        [ForeignKey("Planification_JourneeBase")]
        public int PlanificationProductionBase_PlanificationJourneeID { get; set; }
        public ProduitBase ProduitBase { get; set; }
        public PlanificationJourneeBase Planification_JourneeBase { get; set; }
    }
}
