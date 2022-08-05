using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string Nom_Complet { get; set; }
        [Required(ErrorMessage = "Phone Number Name is required")]
        public string GSM { get; set; }
        [Required(ErrorMessage = "Adresse Name is required")]
        public string Adresse { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        public string Logo { get; set; }

        public int? Abonnement_ID { get; set; }
        public int? LieuStockage_ID { get; set; }
        public Abonnement_ClientModel Abonnement_Client { get; set; }
    }
}
