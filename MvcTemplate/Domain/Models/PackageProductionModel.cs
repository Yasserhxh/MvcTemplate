using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PackageProductionModel
    {
        //public PackageProductionModel()
        //{
        //    detailsProduction = new List<DetailsProductionModel>();
        //}
        public int PackageProduction_ID { get; set; }
        public int PackageProduction_ProduitPackageID { get; set; }
        public int PackageProduction_ProduitID { get; set; }
        public decimal PackageProduction_Quantite { get; set; }
        public DateTime PackageProduction_DateCreation { get; set; }
        public int PackageProduction_AbonnementID { get; set; }
        public string PackageProduction_UtilisateurID { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public ProduitPackageModel ProduitPackage { get; set; }
        //public List<DetailsProductionModel> detailsProduction { get; set; }
    }
}
