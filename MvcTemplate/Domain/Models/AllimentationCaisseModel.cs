using System;
using System.Collections.Generic;
using System.Text;
using Domain.Authentication;

namespace Domain.Models
{
    public class AllimentationCaisseModel
    {
        public int AllimentationCaisse_ID { get; set; }
        public string AllimentationCaisse_UtilsateurID { get; set; }
        public decimal AllimentationCaisse_Montant { get; set; }
        public int AllimentationCaisse_PointVenteID { get; set; }
        public int AllimentationCaisse_PositionVenteID { get; set; }
        public int AllimentationCaisse_AbonnementID { get; set; }
        public int AllimentationCaisse_FlagCloture { get; set; }
        public DateTime AllimentationCaisse_DateCreation { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public ApplicationUser User { get; set; }

    }
}
