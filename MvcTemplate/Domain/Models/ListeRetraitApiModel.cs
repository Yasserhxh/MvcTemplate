using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ListeRetraitApiModel
    {
        public IEnumerable<RetraitCaisseModel> RetraitCaisseList { get; set; }
        public IEnumerable<RetraitTypeModel> RetraitCaisseType { get; set;}
        public decimal Montant { get; set; }
    }
}
