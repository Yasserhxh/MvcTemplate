using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Package_FormeModel
    {
        public Package_FormeModel()
        {
            details = new List<PackageForme_DetailsModel>();
            detailsMatiere = new List<PackageFormeDetails_MatiereModel>();
        }
        public int PackageForme_ID { get; set; }
        public int PackageForme_FormeProduitID { get; set; }
        public int PackageForme_ProduitPackageID { get; set; }
        public DateTime PackageForme_DateCreation { get; set; }
        public int PackageForme_AbonnementID { get; set; }
        public bool PackageForme_IsInUse { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
        public ProduitPackageModel ProduitPackage { get; set; }
        public List<PackageForme_DetailsModel> details { get; set; }
        public List<PackageFormeDetails_MatiereModel> detailsMatiere { get; set; }
    }
}
