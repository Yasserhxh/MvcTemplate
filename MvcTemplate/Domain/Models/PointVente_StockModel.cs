using System;

namespace Domain.Models
{
    public class PointVente_StockModel
    {
        public int PointVenteStock_Id { get; set; }
        public int PointVenteStock_ProduitID { get; set; }
        public int PointVenteStock_FormeProduitID { get; set; }
        public int PointVenteStock_PointVenteID { get; set; }
        public decimal PointVenteStock_QuantiteProduit { get; set; }
        public DateTime PointVenteStock_DateModification { get; set; }
        public int PointVenteStock_AbonnementID { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
        public ProduitVendableModel Produit_Vendable { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
    }
}
