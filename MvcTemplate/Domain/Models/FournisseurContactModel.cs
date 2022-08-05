namespace Domain.Models
{
    public class FournisseurContactModel
    {
        public int FournisseurContact_ID { get; set; }
        public string FournisseurContact_Nom { get; set; }
        public int FournisseurContact_FonctionID { get; set; }
        public string FournisseurContact_Email { get; set; }
        public string FournisseurContact_GSM { get; set; }
        public int? FournisseurContact_FournisseurProduitID { get; set; }
        public int? FournisseurContact_FournisseurID { get; set; }
        public FournisseurModel Founisseur { get; set; }
        public FournisseurProduitsModel Fournisseur_Produits { get; set; }
        public FonctionModel Fonction { get; set; }
    }
}
