using System;

namespace Domain.Models
{
    public class ProduitConsomableStokageModel   
    {           
        public int ProduitConsomableStokage_Id { get; set; }
        public int ProduitConsomableStokage_LieuStockID { get; set; }           
        public int ProduitConsomableStokage_ProduitVendableId { get; set; }
        public int ProduitConsomableStokage_FormeProduitId { get; set; }
        public decimal ProduitConsomableStokage_StockMinimum { get; set; }
        public decimal ProduitConsomableStokage_StockMaximum { get; set; }
        public int ProduitConsomableStokage_IsActive { get; set; }
        public int ProduitConsomableStokage_AbonnementId { get; set; }
        public DateTime ProduitConsomableStokage_DateCreation { get; set; }
        public decimal ProduitConsomableStokage_QuantiteActuelle { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public ProduitPretConsomerModel Produit_PretConsomer { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        
    }
}


