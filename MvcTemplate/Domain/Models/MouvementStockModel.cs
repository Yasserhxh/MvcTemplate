using System;

namespace Domain.Models
{
    public class MouvementStockModel
    {
        public int MouvementStock_Id { get; set; }
        public int MouvementStock_MatierePremiereStokageId { get; set; }
        public DateTime MouvementStock_Date { get; set; }
        public int MouvementStock_MouvementTypeId { get; set; }
        public decimal MouvementStock_Quantite { get; set; }
        public int MouvementStock_UniteMesureId { get; set; }
        public int? MouvementStock_FournisseurId { get; set; }
        public DateTime MouvementStock_DateSaisie { get; set; }
        public decimal MouvementStock_PrixAchatUnite { get; set; }
        public int? MouvementStock_DestinationStockId { get; set; }
        public bool MouvementStock_ReceptionStatutId { get; set; }
        public DateTime MouvementStock_DateReception { get; set; }
        public int MouvementStock_AbonnementId { get; set; }
        public DateTime MouvementStock_DateCreation { get; set; }
        public decimal MouvementStock_MatiereQuantiteActuelle { get; set; }
        public int MouvementStock_IsActive { get; set; }

        public MatierePremiereStockageModel MatierePremiere_Stokage { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public FournisseurModel Founisseur { get; set; }
        public MouvementTypeModel Mouvement_Type { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }

    }
}
