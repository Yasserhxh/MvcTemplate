using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Fournisseur_Produits")]
    public class FournisseurProduits
    {
        public FournisseurProduits()
        {
            Fournisseur_Contact = new Collection<FournisseurContact>();
            ProduitConso_Link = new Collection<Fournisseur_ProduitConso>();
        }
        [Key]
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
        [ForeignKey("Ville")]
        public int FournisseurProduitConso_VilleID { get; set; }
        public ICollection<Fournisseur_ProduitConso> ProduitConso_Link { get; set; }
        public ICollection<FournisseurContact> Fournisseur_Contact { get; set; }
        public Ville Ville { get; set; }
    }
}
