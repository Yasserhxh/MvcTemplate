using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MouvementCaisseModel
    {
        public int MouvementsCaisse_ID { get; set; }
        public int MouvementsCaisse_TypeID { get; set; }
        public decimal MouvementsCaisse_Montant { get; set; }
        public DateTime MouvementsCaisse_DateMouvement { get; set; }
        public int MouvementsCaisse_PositionVenteID { get; set; }
        public string MouvementsCaisse_UtilisateurID { get; set; }
        public int MouvementsCaisse_AbonnementID { get; set; }
        public MouvementCaisse_TypeModel MouvementCaisse_Type { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
    }
}
