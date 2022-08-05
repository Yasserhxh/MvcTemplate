namespace Domain.Models
{
    public class PointVente_ProduitDetailsModel
    {
        public int PointVenteProduitDetails_ID { get; set; }
        public int PointVenteProduitDetails_PdvStockID { get; set; }
        public decimal PointVenteProduitDetails_Quantite { get; set; }
        public decimal PointVenteProduitDetails_PrixProduit { get; set; }
        public PointVente_StockModel PointVente_Stock { get; set; }
    }
}
