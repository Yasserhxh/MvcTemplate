namespace Domain.Models
{
    public class SousFamilleModel
    {
        public int SousFamille_ID { get; set; }
        public string SousFamille_Libelle { get; set; }
        public int SousFamille_ParentID { get; set; }
        public string SousFamille_Image { get; set; }
        public int SousFamille_AbonnementID { get; set; }
        public FamilleProduitModel Famille_Produit { get; set; }
    }
}
