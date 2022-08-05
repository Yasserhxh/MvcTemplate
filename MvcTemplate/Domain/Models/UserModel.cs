using Domain.Entities;
using System;

namespace Domain.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Gsm { get; set; }
        public string Client { get; set; }
        public int? Message { get; set; }
        public int? Statut { get; set; }
        public string Logo { get; set; }
        public Point_Vente Point_Vente { get; set; }
        public Atelier Atelier { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public PositionVente Position_Vente { get; set; }
        public string Affectation { get; set; }
    }
}
