using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ticketFormes
    {
        public string formeLibelle { get; set; }
        public decimal Quantité { get; set; }
        public decimal prixUnité { get; set; }
        public decimal prixTotal { get; set; }
    }
}
