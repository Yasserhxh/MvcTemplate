namespace Domain.Models
{
    public class Abonnement_ClientModel
    {
        public int Abonnement_Id { get; set; }
        public string Abonnement_NomClient { get; set; }
        public string Abonnement_RegistreCommercial { get; set; }
        public string Abonnement_IdentifiantFiscal { get; set; }
        public string Abonnement_ICE { get; set; }
        public string Abonnement_Adresse { get; set; }
        public int Abonnement_VilleId { get; set; }
        public string Abonnement_Telephone { get; set; }
        public string Abonnement_ContactNomPrenom { get; set; }
        public string Abonnement_ContactTelephone { get; set; }
        public string Abonnement_ContactEmail { get; set; }
        public int Abonnement_IsActive { get; set; }
        public string Abonnement_Logo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
