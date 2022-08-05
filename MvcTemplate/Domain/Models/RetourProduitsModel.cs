using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class RetourProduitsModel
    {
        public RetourProduitsModel()
        {
            retourDetails = new List<RetourDetailsModel>();
        }
        public int Retour_Id { get; set; }
        public int Retour_PositionVenteID { get; set; }
        public decimal Retour_PrixTotal { get; set; }
        public DateTime Retour_DateCreation { get; set; }
        public string Retour_UtilisateurID { get; set; }
        public int Retour_AbonnementID { get; set; }
        public int Retour_FlagCloture { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public List<RetourDetailsModel> retourDetails { get; set; }
    }
}
