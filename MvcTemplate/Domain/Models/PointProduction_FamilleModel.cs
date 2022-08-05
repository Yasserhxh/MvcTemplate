using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PointProduction_FamilleModel
    {
        public int Id { get; set; }
        public int PointProduction_ID { get; set; }
        public int FamilleProduit_ID { get; set; }
        public int Abonnement_Id { get; set; }
        public int Is_Active { get; set; }
        public int Abonnement_ID { get; set; }
        public DateTime DateCreation { get; set; }
        public AtelierModel Atelier { get; set; }
        public FamilleProduitModel Famille_Produit { get; set; }
    }
}
