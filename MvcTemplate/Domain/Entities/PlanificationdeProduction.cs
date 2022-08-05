using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Planification_Production")]
    public class PlanificationdeProduction
    {
        [Key]
        public int PlanificationProduction_Id { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationProduction_Date { get; set; }
        [ForeignKey("Produit_Vendable")]
        public int PlanificationProduction_ProduitVendableId { get; set; }
        [ForeignKey("Forme_Produit")]
        public int PlanificationProduction_FormeProduitId { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PlanificationProduction_QuantitePrevue { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PlanificationProduction_QuantiteProduite { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string PlanificationProduction_Motif { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal PlanificationProduction_CoutRevient { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PlanificationProduction_QuantiteRestante { get; set; }
        [Column(TypeName = "int")]
        public int PlanificationProduction_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationProduction_DateCreation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanificationProduction_DateModification { get; set; }
        [ForeignKey("Planification_Journee")]
        public int PlanificationProduction_PlanificationJourneeID { get; set; }
        public ProduitVendable Produit_Vendable { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public PlanificationJournee Planification_Journee { get; set; }
    }
}
