using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Taux_TVAModel
    {
        public int TauxTVA_Id { get; set; }
        public decimal TauxTVA_Pourcentage { get; set; }
        public int TauxTVA_AbonnementId { get; set; }
        public DateTime TauxTVA_DateCreation { get; set; }
    }
}
