using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Commande_PaiementModel
    {
        public int CommandePaiement_ID { get; set; }
        public int CommandePaiement_CommandeID { get; set; }
        public int CommandePaiement_ModePaiementID { get; set; }
        public decimal CommandePaiement_Montant { get; set; }
        public int CommandePaiement_PositionVenteID { get; set; }
        public string CommandePaiement_UtilisateurID { get; set; }
        public int CommandePaiement_FlagCloture { get; set; }
        public DateTime CommandePaiement_DatePaiement { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public CommandeModel Commande { get; set; }
        public ModePaiementModel Mode_Paiement { get; set; }
    }
}
