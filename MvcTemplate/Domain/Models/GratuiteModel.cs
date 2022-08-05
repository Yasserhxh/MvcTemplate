using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class GratuiteModel
    {
        public GratuiteModel()
        {
            details = new List<GratuiteDetailsModel>(); 
        }
        public int Gratuite_ID { get; set; }
        public decimal Gratuite_CoutDeRevientTotal { get; set; }
        public DateTime Gratuite_DateCreation { get; set; }
        public int Gratuite_PositionVente { get; set; }
        public int Gratuite_AbonnementID { get; set; }
        public string Gratuite_UtilisateurID { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public List<GratuiteDetailsModel> details { get; set; }
    }
}
