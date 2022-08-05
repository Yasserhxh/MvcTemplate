using System;
using System.Collections.Generic;
using System.Text;
using Domain.Authentication;

namespace Domain.Models
{
    public class ClotureJourneeModel
    {
        public int ClotueJournee_ID { get; set; }
        public int ClotueJournee_PositionVenteID { get; set; }
        public decimal ClotueJournee_SoldeDebit { get; set; }
        public decimal ClotueJournee_SoldeCredit { get; set; }
        public decimal ClotueJournee_Montant { get; set; }
        public decimal ClotueJournee_Alimentation { get; set; }
        public string ClotueJournee_Description { get; set; }
        public string ClotueJournee_UtilisateurID { get; set; }
        public DateTime ClotueJournee_DateCreation { get; set; }
        public int ClotueJournee_AbonnementID { get; set; }
        public int ClotueJournee_Valide { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public ApplicationUser User { get; set; }

    }
}
