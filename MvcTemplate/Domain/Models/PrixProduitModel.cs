namespace Domain.Models
{
    public class PrixProduitModel
    {
        public int PrixProduit_Id { get; set; }
        public string PrixProduit_Description { get; set; }
        public int PrixProduit_FormeProduitId { get; set; }
        public decimal PrixProduit_Prix { get; set; }
        public int? PrixProduit_TauxTVAId { get; set; }
        public decimal PrixProduit_QuantiteMinimale { get; set; }
        public int PrixProduit_IsActive { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }

    }
}
