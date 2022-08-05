namespace Domain.Models
{
    public class DetailsProduitApproModel
    {
        public int DetailsProduitAppro_ID { get; set; }
        public int DetailsProduitAppro_ProduitApproID { get; set; }
        public decimal DetailsProduitAppro_QuantiteProduite { get; set; }
        public decimal DetailsProduitAppro_PrixProduit { get; set; }
        public ProduitApproModel ProduitAppro { get; set; }
    }
}
