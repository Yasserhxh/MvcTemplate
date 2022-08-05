using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class ProduitPackageModel
    {
        public ProduitPackageModel()
        {
            Sous_ProduitPackage = new List<Sous_ProduitPackageModel>();
            SousMatierePackages = new List<SousMatierePackageModel>();
            formes = new List<Forme_ProduitModel>();
        }
        public int ProduitPackage_ID { get; set; }
        public string ProduitPackage_Designation { get; set; }
        public string ProduitPackage_Photo { get; set; }
        public int ProduitPackage_UniteVente { get; set; }
        public int ProduitPackage_FamilleID { get; set; }
        public decimal ProduitPackage_Quantite { get; set; }
        public decimal ProduitPackage_CoutdeRevient { get; set; }
        public DateTime ProduitPackage_DateCreation { get; set; }
        public int ProduitPackage_AbonnementID { get; set; }
        public List<Sous_ProduitPackageModel> Sous_ProduitPackage { get; set; }
        public List<SousMatierePackageModel> SousMatierePackages { get; set; }
        public List<Forme_ProduitModel> formes { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public SousFamilleModel Sous_Famille { get; set; }
    }
}
