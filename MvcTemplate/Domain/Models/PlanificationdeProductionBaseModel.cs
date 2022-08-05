using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PlanificationdeProductionBaseModel
    {
        public int PlanificationProductionBase_Id { get; set; }
        public DateTime PlanificationProductionBase_Date { get; set; }
        public int PlanificationProductionBase_ProduitBase { get; set; }
        public decimal PlanificationProductionBase_QuantitePrevue { get; set; }
        public decimal PlanificationProductionBase_QuantiteProduite { get; set; }
        public decimal PlanificationProductionBase_CoutRevient { get; set; }
        public decimal PlanificationProductionBase_QuantiteRestante { get; set; }
        public int PlanificationProductionBase_AbonnementId { get; set; }
        public DateTime PlanificationProductionBase_DateCreation { get; set; }
        public DateTime? PlanificationProductionBase_DateModification { get; set; }
        public int PlanificationProductionBase_PlanificationJourneeID { get; set; }
        public ProduitBaseModel ProduitBase { get; set; }
        public PlanificationJourneeBaseModel Planification_JourneeBase { get; set; }
    }
}
