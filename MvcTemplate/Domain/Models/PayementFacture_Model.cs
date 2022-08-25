using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class PayementFacture_Model
    {
        public int PayementFacture_ID { get; set; }
        public int PayementFacture_FactureID { get; set; }
        public decimal PayementFacture_Montant { get; set; }
        public string PayementFacture_Methode { get; set; }
        public string PayementFacture_Informations { get; set; }
        public DateTime? PayementFacture_DateEcheance { get; set; }
        public DateTime? PayementFacture_DatePayement { get; set; }
        public DateTime? PayementFacture_DateSaisie { get; set; }
        public int PayementFacture_AbonnementID { get; set; }
        public string PayementFacture_CreePar { get; set; }
        public FactureModel Facture { get; set; }
    }
}
