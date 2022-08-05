using System;

namespace Domain.Models
{
    public class Statut_LivraisonModel
    {
        public int StatutCommande_Id { get; set; }
        public string StatutCommande_Libelle { get; set; }
        public int StatutCommande_AbonnementId { get; set; }
        public DateTime StatutCommande_DateCreation { get; set; }
    }
}
