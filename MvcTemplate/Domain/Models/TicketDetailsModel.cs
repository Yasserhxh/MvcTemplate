using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TicketDetailsModel
    {
        public TicketDetailsModel()
        {
          //  formes = new List<ticketFormes>(); 
        }
        public string produitImage { get; set; }
        public string produitLibelle { get; set; }
        public string formeLibelle { get; set; }
        public decimal Quantité { get; set; }
        public decimal prixUnité { get; set; }
        public decimal prixTotal { get; set; }
       // public List<ticketFormes> formes { get; set; }
    }
}
