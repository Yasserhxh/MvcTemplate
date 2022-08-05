using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PerteModel
    {
        public PerteModel()
        {
            details = new List<PerteDetailsModel>();
        }
        public int Perte_ID { get; set; }
        public DateTime Perte_DatePerte { get; set; }
        public decimal Perte_ValeurTotal { get; set; }
        public int Perte_AbonnementID { get; set; }
        public int Perte_PositionVenteID { get; set; }
        public string Perte_UtilisateurID { get; set; }
        public DateTime Perte_DateCreation { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public List<PerteDetailsModel> details { get; set; }
    }
}
