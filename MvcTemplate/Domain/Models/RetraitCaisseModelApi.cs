using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public  class RetraitCaisseModelApi
    {
        public decimal montantRetrait { get; set; } 
        public string motifRetrait { get; set; }
        public string userID { get; set; } 
        public int typeRetrait { get; set; }
    }
}
