using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CommandeApiModel
    {
        public CommandeApiModel()
        {
            details = new List<CommandeDetailApiModel>();
        }
        public int Commande_Id { get; set; }
        public int commande_EtatDeLivraison { get; set; }
        public string Commande_NomVendeur { get; set; }
        public string Commande_NomDemandeurs { get; set; }
        public string Commande_NumeroTel { get; set; }
        public string Commande_NumeroTicket { get; set; }
        public DateTime Commande_DateLivraisonPrevue { get; set; }
        public int Commande_EtatDePaiement { get; set; }
        public decimal Commande_TauxRemise { get; set; }
        public decimal Commande_MontantSansRemise { get; set; }
        public decimal Commande_MontantTotal { get; set; }
        public decimal Commande_Avance { get; set; }
        public decimal commande_ResteAPayer { get; set; }
        public Statut_LivraisonModel statut_Livraison { get; set;}
        public Statut_PaiementCommandeModel statut_Paiement { get; set; }
        public List<CommandeDetailApiModel> details { get; set; }
    }
}
