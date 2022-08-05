using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class FournisseurModel
    {
        public FournisseurModel()
        {
            Fournisseur_Contact = new List<FournisseurContactModel>();
        }
        public int Founisseur_Id { get; set; }
        public string Founisseur_RaisonSocial { get; set; }
        public string Founisseur_ICE { get; set; }
        public string Founisseur_Adresse { get; set; }
        public string Founisseur_NomContact { get; set; }
        public string Founisseur_Telephone { get; set; }
        public DateTime? Founisseur_DateSaisie { get; set; }
        public string Founisseur_UtilisateurId { get; set; }
        public int Founisseur_AbonnementId { get; set; }
        public DateTime? Founisseur_DateCreation { get; set; }
        public int? Founisseur_IsActive { get; set; }
        public int? Founisseur_VilleID { get; set; }

        public List<FournisseurContactModel> Fournisseur_Contact { get; set; }
        public List<FournisseurMatiereModel> MatiereLink { get; set; }

        public VilleModel Ville { get; set; }

    }
}
