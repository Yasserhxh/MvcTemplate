using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PackageForme_DetailsModel
    {
        public int PackageFormeDetails_ID { get; set; }
        public int PackageFormeDetails_PackageFormeID { get; set; }
        public int PackageFormeDetails_SousProduitID { get; set; }
        public decimal PackageFormeDetails_Quantite { get; set; }
        public decimal PackageFormeDetails_CoutdeRevient { get; set; }
        public Package_FormeModel Package_Forme { get; set; }
        public Sous_ProduitPackageModel Sous_ProduitPackage { get; set; }
    }
}
