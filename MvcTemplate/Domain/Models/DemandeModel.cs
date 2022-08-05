using System;

namespace Domain.Models
{
    public class DemandeModel
    {
        public int Demande_ID { get; set; }
        public string Demande_Type { get; set; }
        public int? Demande_PlanificationID { get; set; }
        public int Demande_BonDeSortieID { get; set; }
        public int? Demande_LieuStockageID { get; set; }
        public int Demande_AtelierID { get; set; }
        public DateTime Demande_DateCreation { get; set; }
        public int Demande_AbonnementID { get; set; }
        public string Demande_Etat { get; set; }
        public string Demande_Motif { get; set; }
        public string Demande_UtilisateurID { get; set; }
        public string Demande_ValideParID { get; set; }
        public BonDeSortieModel BonDe_Sortie { get; set; }
        public AtelierModel Atelier { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public PlanificationJourneeModel Planification_Journee { get; set; }   

    }
}
