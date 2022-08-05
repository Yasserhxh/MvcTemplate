namespace Domain.Models
{
    public class FicheDetailsModel
    {
        public string FicheTechnique_MatierePremiere { get; set; }
        public decimal FicheTechnique_QuantiteMatiere { get; set; }
        public string FicheTechnique_UniteMesure { get; set; }
        public decimal FicheTechnique_Prix { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
        public ProduitVendableModel Produit_Vendable { get; set; }

    }
}
