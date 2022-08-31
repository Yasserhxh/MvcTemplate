using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class DistributeurModel
    {
        public int Distributeur_ID { get; set; }
        public string Distributeur_RaisonSocial { get; set; }
        public int Distributeur_ICE { get; set; }
        public int Distributeur_IF { get; set; }
        public string Distributeur_Adresse { get; set; }
        public string Distributeur_NomContact { get; set; }
        public string Distributeur_Telephone { get; set; }
        public int Distributeur_IsActive { get; set; }
        public DateTime Distributeur_DateSaisie { get; set; }
        public int Distributeur_AbonnementID { get; set; }
        public DateTime Distributeur_DateCreation { get; set; }
    }
}
