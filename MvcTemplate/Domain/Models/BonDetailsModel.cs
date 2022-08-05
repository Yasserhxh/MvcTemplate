namespace Domain.Models
{
    public class BonDetailsModel
    {
        public int BonDetails_ID { get; set; }
        public decimal BonDeSortie_Quantite { get; set; }
        public decimal BonDeSortie_QuantiteEnStock { get; set; }
        public decimal BonDeSortie_QuantiteDemandee { get; set; }
        public decimal BonDeSortie_QuantiteLivree { get; set; }
        public int BonDeSortie_UniteMesureId { get; set; }
        public int BonDeSortie_MatiereId { get; set; }
        public int BonDeSortie_BonDeSortieID { get; set; }
        public string BonDeSortie_MatiereLibelle { get; set; }
        public string BonDeSortie_UniteLibelle { get; set; }
        public string BonDeSorite_UniteStock { get; set; }
        public decimal BonDeSortie_QuantiteEnStockAvecPlan { get; set; }
        public BonDeSortieModel BonDe_Sortie { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
    }
}
