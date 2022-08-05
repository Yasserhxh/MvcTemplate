using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PointVente_FamilleModel
    {
        public int Id { get; set; }
        public int PointVente_Id { get; set; }
        public int FamilleProduit_Id { get; set; }
        public int Abonnement_Id { get; set; }
        public int IsActive { get; set; }
        public DateTime DateCreation { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
        public FamilleProduitModel Famille_Produit { get; set; }
    }
}
