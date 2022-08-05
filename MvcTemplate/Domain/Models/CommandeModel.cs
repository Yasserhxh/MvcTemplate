using Domain.Authentication;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class CommandeModel
    {
        public CommandeModel()
        {
            details = new List<CommandeDetailModel>();
            Tva = new List<TvaModel>();
        }
        public int Commande_Id { get; set; }
        public DateTime Commande_Date { get; set; }
        public string Commande_NomDemandeurs { get; set; }
        public string Commande_NumeroTel { get; set; }
        public int? Commande_PointVenteId { get; set; }
        public DateTime Commande_DateLivraisonPrevue { get; set; }
        public int Commande_EtatDePaiement { get; set; }
        public decimal Commande_MontantTotal { get; set; }
        public decimal Commande_TauxdeRemise { get; set; }
        public decimal Commande_MontantSansRemise { get; set; }
        public decimal Commande_Avance { get; set; }
        public DateTime Commande_DateCreation { get; set; }
        public int Commande_EtatDeLivraison { get; set; }
        public string Commande_UtilisateurCommandeId { get; set; }
        public string Commande_UtilisateurLivraisonId { get; set; }
        public int? Commande_CaissePayementId { get; set; }
        public int Commande_AbonnementId { get; set; }
        public string Commande_NumeroTicket { get; set; }

        public int? Commande_CaisseCommandeId { get; set; }
        public List<CommandeDetailModel> details { get; set; }
        public List<TvaModel> Tva { get; set; }
        //public Point_VenteModel Point_Vente { get; set; }
        public Statut_LivraisonModel Statut_Livraison { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
        public Statut_PaiementCommandeModel Statut_PaiementCommande { get; set; }
        public ApplicationUser User { get; set; }

    }
}
