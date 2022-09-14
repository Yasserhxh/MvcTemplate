using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Demande_Approv")]
    public class DemandeApprov
    {
        public DemandeApprov()
        {
            details = new Collection<DemandeApprov_Details>();
        }
        [Key]
        public int DemandeApprov_ID { get; set; }
        public DateTime DemandeApprov_DateCreation { get; set; }
        public DateTime DemandeApprov_DateLivraisonPrevue { get; set; }
        public string DemandeApprov_EtatDemande { get; set; }
        public string DemandeApprov_VailidePar { get; set; }
        public DateTime? DemandeApprov_DateValidation { get; set; }
        public int DemandeApprov_AbonnementID { get; set; }
        public ICollection<DemandeApprov_Details> details { get; set; }
    }
}
