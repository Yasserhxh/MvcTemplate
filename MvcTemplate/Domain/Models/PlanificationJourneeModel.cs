using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class PlanificationJourneeModel
    {
        public PlanificationJourneeModel()
        {
            Planification_Production = new List<PlanificationdeProductionModel>();
        }
        public int PlanificationJournee_ID { get; set; }
        public string Planification_GeneratedID { get; set; }
        public DateTime PlanificationJournee_Date { get; set; }
        public string PlanificationJournee_Etat { get; set; }
        public decimal PlanificationJournee_CoutDeRevient { get; set; }
        public DateTime PlanificationJournee_DateCreation { get; set; }
        public int PlanificationJournee_AbonnementID { get; set; }
        public int PlanificationJournee_BonDeSortieID { get; set; }
        public int? PlanificationJournee_AtelierID { get; set; }
        public int? PlanificationJournee_LieuStockageID { get; set; }
        public string PlanificationJournee_UtilisateurId { get; set; }
        public string PlanificationJournee_ValidePar { get; set; }
        public bool? PlanificationJournee_SeenByStock { get; set; }
        public bool? PlanificationJournee_SeenByAtelier { get; set; }
        public List<PlanificationdeProductionModel> Planification_Production { get; set; }
        public BonDeSortieModel BonDe_Sortie { get; set; }
        public AtelierModel Atelier { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }

    }
}
