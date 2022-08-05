using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Paiement_Abonnement")]
    public class Paiement_Abonnement
    {
        [Key]
        public int PaiementAbonnement_ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PaiementAbonnement_Date { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal PaiementAbonnement_Montant { get; set; }
         [Column(TypeName = "decimal(8,2)")]
        public decimal PaiementAbonnement_PeriodeDePaiement { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PaiementAbonnement_DateDebutPeriode { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PaiementAbonnement_DateFinPeriode { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? PaiementAbonnement_DateSaisie { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string PaiementAbonnement_AdminUserId { get; set; }
        [ForeignKey("Abonnemet_Client")]
        public int PaiementAbonnement_AbonnementId { get; set; }   
        [ForeignKey("Point_Vente")]
        public int PaiementAbonnement_PointVenteID { get; set; }
        public Abonnement_Client Abonnemet_Client { get; set; }
        public Point_Vente Point_Vente { get; set; }

    }
}
