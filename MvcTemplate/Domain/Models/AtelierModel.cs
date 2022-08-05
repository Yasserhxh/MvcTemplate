using System;

namespace Domain.Models
{
    public class AtelierModel
    {
        public int Atelier_ID { get; set; }
        public string Atelier_Nom { get; set; }
        public string Atelier_Adresse { get; set; }
        public string Atelier_NomResponsable { get; set; }
        public string Atelier_Telephone { get; set; }
        public int Atelier_IsActive { get; set; }
        public string Atelier_UTILISATEUR { get; set; }
        public int Atelier_AbonnementID { get; set; }
        public DateTime Atelier_DateCreation { get; set; }
        public int Atelier_VilleID { get; set; }
        public int Atelier_CodePostal { get; set; }
        public int? Atelier_StockID { get; set; }
        public VilleModel Ville { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }

    }
}
