using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Echange_ProduitsModel
    {
        public Echange_ProduitsModel()
        {
            details = new List<EchangeProduit_DetailsModel>();
        }
        public int EchangeProduits_ID { get; set; }
        public string EchangeProduits_AdminID { get; set; }
        public int EchangeProduits_PdvFournisseurID { get; set; }
        public int EchangeProduits_PdvRecepID { get; set; }
        public string EchangeProduits_Statut { get; set; }
        public string EchangeProduits_PdvFournisseurUserID { get; set; }
        public string EchangeProduits_PdvRecepUserID { get; set; }
        public DateTime EchangeProduits_DateEchange { get; set; }
        public int EchangeProduits_AbonnementID { get; set; }
        public ApplicationUser User { get; set; }
        public virtual Point_VenteModel FournisseurPdv { get; set; }
        public virtual Point_VenteModel ReceptionPdv { get; set; }
        public List<EchangeProduit_DetailsModel> details { get; set; }
    }
}
