using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Authentication
{
    public class ApplicationUser : IdentityUser
    {

        [ForeignKey("Abonnemet_Client")]
        public int? Abonnement_ID { get; set; }
        public int? Abonnement_ISACTIVE { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int? LieuStockage_ID { get; set; }
        [ForeignKey("Atelier")]
        public int? AtelierID { get; set; }
        [ForeignKey("Point_Vente")]
        public int? PointVente_ID { get; set; } 
        [ForeignKey("Position_Vente")]
        public int? PositionVente_ID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Nom { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Prenom { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Nom_Complet { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Logo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Adresse { get; set; }
        public Abonnement_Client abonnement_Client { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public Atelier Atelier { get; set; }
        public Point_Vente Point_Vente { get; set; }
        public PositionVente Position_Vente { get; set; }
    }
}
