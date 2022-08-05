using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Approvisionnement_ProduitConsoModel
    {
        public int ApprovisionnementProduit_Id { get; set; }
        public DateTime ApprovisionnementProduit_Date { get; set; }
        public int ApprovisionnementProduit_PointVenteID { get; set; }
        public int ApprovisionnementProduit_StockID { get; set; }
        public int ApprovisionnementProduit_ProduitStockageId { get; set; }
        public decimal ApprovisionnementProduit_Quantite { get; set; }
        public int? ApprovisionnementProduit_UniteMesureId { get; set; }
        public DateTime? ApprovisionnementProduit_DateReception { get; set; }
        public DateTime ApprovisionnementProduit_DateCreation { get; set; }
        public string ApprovisionnementProduit_Etat { get; set; }
        public string ApprovisionnementProduit_LieuUserId { get; set; }
        public string ApprovisionnementProduit_PointVenteUserId { get; set; }
        public int ApprovisionnementProduit_AbonnementId { get; set; }

        public Point_VenteModel Point_Vente { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public ProduitConsomableStokageModel ProduitConsomable_Stokage { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
