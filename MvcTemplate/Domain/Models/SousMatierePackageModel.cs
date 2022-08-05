using System;

namespace Domain.Models
{
    public class SousMatierePackageModel
    {
        public int SousMatierePackage_ID { get; set; }
        public int SousMatierePackage_MatiereID { get; set; }
        public int SousMatierePackage_ProduitPackageID { get; set; }
        public int SousMatierePackage_AbonnementID { get; set; }
        public DateTime SousMatierePackage_DateCreation { get; set; }
        public MatierePremiereModel Matiere_Premiere { get; set; }
        public ProduitPackageModel ProduitPackage { get; set; }
    }
}
