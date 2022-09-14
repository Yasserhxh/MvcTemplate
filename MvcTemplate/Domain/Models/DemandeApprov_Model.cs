using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class DemandeApprov_Model
    {
        public DemandeApprov_Model()
        {
            details = new List<DemandeApprov_DetailsModel>();
        }
        public int DemandeApprov_ID { get; set; }
        public DateTime DemandeApprov_DateCreation { get; set; }
        public DateTime DemandeApprov_DateLivraisonPrevue { get; set; }
        public string DemandeApprov_EtatDemande { get; set; }
        public string DemandeApprov_VailidePar { get; set; }
        public DateTime? DemandeApprov_DateValidation { get; set; }
        public int DemandeApprov_AbonnementID { get; set; }
        public List<DemandeApprov_DetailsModel> details { get; set; }
    }
}
