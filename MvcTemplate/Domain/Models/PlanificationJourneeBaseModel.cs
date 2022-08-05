using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PlanificationJourneeBaseModel
    {
        public PlanificationJourneeBaseModel()
        {
            Planification_ProductionBase = new List<PlanificationdeProductionBaseModel>();
        }
        public int PlanificationJourneeBase_ID { get; set; }
        public DateTime PlanificationJourneeBase_Date { get; set; }
        public string PlanificationJourneeBase_Etat { get; set; }
        public decimal PlanificationJourneeBase_CoutDeRevient { get; set; }
        public DateTime PlanificationJourneeBase_DateCreation { get; set; }
        public int PlanificationJourneeBase_AbonnementID { get; set; }
        public int PlanificationJourneeBase_BonDeSortieID { get; set; }
        public int? PlanificationJourneeBase_AtelierID { get; set; }
        public int? PlanificationJourneeBase_LieuStockageID { get; set; }
        public string PlanificationJourneeBase_UtilisateurId { get; set; }
        public string PlanificationJourneeBase_ValidePar { get; set; }
        public bool? PlanificationJourneeBase_SeenByStock { get; set; }
        public bool? PlanificationJourneeBase_SeenByAtelier { get; set; }
        public List<PlanificationdeProductionBaseModel> Planification_ProductionBase { get; set; }
        public BonDeSortieModel BonDe_Sortie { get; set; }
        public AtelierModel Atelier { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
    }
}
