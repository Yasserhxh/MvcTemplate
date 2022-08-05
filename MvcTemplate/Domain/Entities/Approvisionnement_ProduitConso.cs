using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Approvisionnement_ProduitConsomable")]
    public class Approvisionnement_ProduitConso
    {
        [Key]
        public int ApprovisionnementProduit_Id { get; set; }
        public DateTime ApprovisionnementProduit_Date { get; set; }
        [ForeignKey("Point_Vente")]
        public int ApprovisionnementProduit_PointVenteID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int ApprovisionnementProduit_StockID { get; set; }
        [ForeignKey("ProduitConsomable_Stokage")]
        public int ApprovisionnementProduit_ProduitStockageId { get; set; }
        public decimal ApprovisionnementProduit_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int? ApprovisionnementProduit_UniteMesureId { get; set; }
        public DateTime? ApprovisionnementProduit_DateReception { get; set; }
        public DateTime ApprovisionnementProduit_DateCreation { get; set; }
        public string ApprovisionnementProduit_Etat { get; set; }
        public string ApprovisionnementProduit_LieuUserId { get; set; }
        public string ApprovisionnementProduit_PointVenteUserId { get; set; }
        public int ApprovisionnementProduit_AbonnementId { get; set; }

        public Point_Vente Point_Vente { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public ProduitConsomableStokage ProduitConsomable_Stokage { get; set; }
        public Unite_Mesure Unite_Mesure { get; set;}

    }

}
