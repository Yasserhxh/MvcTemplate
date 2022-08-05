namespace Domain.Models
{
    public class CommandeDetailModel
    {
        public int CommandeDetail_Id { get; set; }
        public int CommandeDetail_CommandeId { get; set; }
        public int CommandeDetail_FormeProduitId { get; set; }
        public decimal CommandeDetail_Quantite { get; set; }
        public int CommandeDetail_UniteId { get; set; }
        public decimal CommandeDetail_Prix { get; set; }
        public int CommandeDetail_AbonnementId { get; set; }
        public decimal CommandeDetail_CoutdeRevient { get; set; }
        public decimal CommandeDetail_Marge { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public CommandeModel Commande { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }

    }
}
