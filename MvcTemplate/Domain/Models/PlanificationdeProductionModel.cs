using System;

namespace Domain.Models
{
    public class PlanificationdeProductionModel
    {
        public int PlanificationProduction_Id { get; set; }
        public DateTime PlanificationProduction_Date { get; set; }
        public int PlanificationProduction_ProduitVendableId { get; set; }
        public int? PlanificationProduction_FormeProduitId { get; set; }
        public decimal PlanificationProduction_QuantitePrevue { get; set; }
        public decimal PlanificationProduction_QuantiteProduite { get; set; }
        public string PlanificationProduction_Motif { get; set; }
        public decimal PlanificationProduction_CoutRevient { get; set; }
        public decimal PlanificationProduction_QuantiteRestante { get; set; }
        public int PlanificationProduction_AbonnementId { get; set; }
        public DateTime PlanificationProduction_DateCreation { get; set; }
        public DateTime? PlanificationProduction_DateModification { get; set; }
        public int PlanificationProduction_PlanificationJourneeID { get; set; }

        public ProduitVendableModel Produit_Vendable { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public PlanificationJourneeModel Planification_Journee { get; set; }
    }
}
