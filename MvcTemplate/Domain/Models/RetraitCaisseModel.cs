using System;
using System.Collections.Generic;
using System.Text;
using Domain.Authentication;

namespace Domain.Models
{
    public class RetraitCaisseModel
    {
        public int RetraitCaisse_ID { get; set; }
        public int RetraitCaisse_TypeRetraitID { get; set; }
        public string RetraitCaisse_Motif { get; set; }
        public decimal RetraitCaisse_Montant { get; set; }
        public int RetraitCaisse_AbonnementID { get; set; }
        public int RetraitCaisse_PositionVenteID { get; set; }
        public string RetraitCaisse_UtilisateurID { get; set; }
        public DateTime RetraitCaisse_DateCreation { get; set; }
        public int RetraitCaisse_FlagCloture { get; set; }
        public RetraitTypeModel Retrait_Type { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public ApplicationUser User { get; set; }

    }
}
