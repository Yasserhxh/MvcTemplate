using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Fournisseur_ProduitConso")]
    public class Fournisseur_ProduitConso
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Fournisseur_Produits")]
        public int FournisseurProduits_Id { get; set; }
        [ForeignKey("Produit_PretConsomer")]
        public int ProduitConsomable_Id { get; set; }
        public int Abonnement_Id { get; set; }
        public int IsActive { get; set; }
        public FournisseurProduits Fournisseur_Produits { get; set; }
        public ProduitPretConsomer Produit_PretConsomer { get; set; }
    }
}
