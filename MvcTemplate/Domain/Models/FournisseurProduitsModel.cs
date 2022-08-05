using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class FournisseurProduitsModel
    {
        public FournisseurProduitsModel()
        {
            Fournisseur_Contact = new List<FournisseurContactModel>();
            ProduitConso_Link = new List<Fournisseur_ProduitConsoModel>();
        }
        public int FournisseurProduitConso_Id { get; set; }
        public string FournisseurProduitConso_RaisonSocial { get; set; }
        public string FournisseurProduitConso_ICE { get; set; }
        public string FournisseurProduitConso_Adresse { get; set; }
        public string FournisseurProduitConso_NomContact { get; set; }
        public string FournisseurProduitConso_TelephoneContact { get; set; }
        public DateTime FournisseurProduitConso_DateCreation { get; set; }
        public int FournisseurProduitConso_IsActive { get; set; }
        public int FournisseurProduitConso_AbonnementID { get; set; }
        public string FournisseurProduitConso_UtilisateurId { get; set; }
        public int FournisseurProduitConso_VilleID { get; set; }
        public List<FournisseurContactModel> Fournisseur_Contact { get; set; }
        public List<Fournisseur_ProduitConsoModel> ProduitConso_Link { get; set; }
        public VilleModel Ville { get; set; }
    }
}
