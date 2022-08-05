using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Fournisseur_ProduitConsoModel
    {
        public int Id { get; set; }
        public int FournisseurProduits_Id { get; set; }
        public int ProduitConsomable_Id { get; set; }
        public int Abonnement_Id { get; set; }
        public int IsActive { get; set; }
        public FournisseurProduitsModel Fournisseur_Produits { get; set; }
        public ProduitPretConsomerModel Produit_PretConsomer { get; set; }
    }
}
