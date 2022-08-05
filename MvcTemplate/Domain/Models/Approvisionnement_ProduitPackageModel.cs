using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Approvisionnement_ProduitPackageModel
    {
        public int ApprovisionnementProduitPackage_Id { get; set; }
        public DateTime ApprovisionnementProduitPackage_Date { get; set; }
        public int ApprovisionnementProduitPackage_PointVenteID { get; set; }
        public int ApprovisionnementProduitPackage_AtelierID { get; set; }
        public int ApprovisionnementProduitPackage_ProduitpackAtelierId { get; set; }
        public decimal ApprovisionnementProduitPackage__Quantite { get; set; }
        public int? ApprovisionnementProduitPackage__UniteMesureId { get; set; }
        public DateTime? ApprovisionnementProduitPackage__DateReception { get; set; }
        public DateTime ApprovisionnementProduitPackage__DateCreation { get; set; }
        public string ApprovisionnementProduitPackage__Etat { get; set; }
        public int ApprovisionnementProduitPackage_AbonnementId { get; set; }

        public Point_VenteModel Point_Vente { get; set; }
        public AtelierModel Atelier { get; set; }
        public ProduitPack_AtelierModel ProduitPack_Atelier { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
    }
}
