using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TicketModel
    {
        public TicketModel()
        {

            Details = new List<TicketDetailsModel>();
            PaiementDetail = new List<Commande_PaiementModel>();
            tva = new List<TvaModel>();

        }
        public string LogoImg { get; set; }
        public string Adresse { get; set; }
        public int IsCommande { get; set; }
        public string ICE { get; set; }
        public string Operation { get; set; }
        public string Tel { get; set; }
        public string NumerTicket { get; set; }
        public string AgentID { get; set; }
        public string AgentName { get; set; }
        public string modePaiement { get; set; }
        public decimal ventePrix { get; set; }
        public decimal remise { get; set; }
        public decimal montantsansremise { get; set; }
        public string commandeLiv { get; set; }
        public string commandepaiement { get; set; }
        public DateTime CommandeDateLiv { get; set; }
        public decimal Avance { get; set; }
        public decimal Reste { get; set; }
        public DateTime VenteDate { get; set; }
        public int aboID { get; set; }
        public List<TicketDetailsModel> Details { get; set; }
        public List<Commande_PaiementModel> PaiementDetail { get; set; }
        public List<TvaModel> tva { get; set; }
    }
}
