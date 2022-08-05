using System;

namespace Domain.Models
{
    public class PaiementAbonnementModel
    {
        public int PaiementAbonnement_ID { get; set; }
        public DateTime PaiementAbonnement_Date { get; set; }
        public decimal PaiementAbonnement_Montant { get; set; }
        public decimal PaiementAbonnement_PeriodeDePaiement { get; set; }
        public DateTime PaiementAbonnement_DateDebutPeriode { get; set; }
        public DateTime PaiementAbonnement_DateFinPeriode { get; set; }
        public DateTime PaiementAbonnement_DateSaisie { get; set; }
        public string PaiementAbonnement_AdminUserId { get; set; }
        public int PaiementAbonnement_AbonnementId { get; set; }
        public int PaiementAbonnement_PointVenteID { get; set; }
        public Abonnement_ClientModel Abonnement_Client { get; set; }
        public Point_VenteModel Point_Vente { get; set; }

    }
}
