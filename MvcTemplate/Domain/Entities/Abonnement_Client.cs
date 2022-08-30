using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Abonnement_Client")]
    public class Abonnement_Client
    {
        [Key]
        public int Abonnement_Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Abonnement_NomClientAR { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Abonnement_ONSSAAuthorisation { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Abonnement_NomClient { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Abonnement_RegistreCommercial { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Abonnement_IdentifiantFiscal { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Abonnement_ICE { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Abonnement_Adresse { get; set; }
        public int Abonnement_VilleId { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Abonnement_Telephone { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Abonnement_ContactNomPrenom { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Abonnement_ContactTelephone { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Abonnement_ContactEmail { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Abonnement_Logo { get; set; }

        public int Abonnement_IsActive { get; set; }
    }
}
