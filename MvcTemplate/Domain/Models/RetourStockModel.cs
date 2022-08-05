using System;

namespace Domain.Models
{
    public class RetourStockModel
    {
        public int RetourStok_ID { get; set; }
        public string RetourStok_Motif { get; set; }
        public int RetourStok_Etat { get; set; }
        public int RetourStok_PlanificationID { get; set; }
        public int RetourStok_BonDeSortieID { get; set; }
        public DateTime RetourStok_DateCreation { get; set; }
        public int RetourStok_AbonnementID { get; set; }
        public string RetourStok_UtilisateurID { get; set; }
        public string RetourStok_ValideParID { get; set; }
        public PlanificationJourneeModel Planification_Journee { get; set; }
        public BonDeSortieModel BonDe_Sortie { get; set; }
    }
}
