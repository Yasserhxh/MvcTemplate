using Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Commande")]
    public class Commande
    {
        public Commande()
        {
            details = new Collection<CommandeDetail>();
            Tva = new Collection<Tva>();
        }
        [Key]
        public int Commande_Id { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Commande_Date { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Commande_NomDemandeurs { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Commande_NumeroTel { get; set; }
        [ForeignKey("Point_Vente")]
        public int? Commande_PointVenteId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Commande_DateLivraisonPrevue { get; set; }
        [ForeignKey("Statut_PaiementCommande")]
        public int Commande_EtatDePaiement { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Commande_MontantTotal { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Commande_TauxdeRemise { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Commande_MontantSansRemise { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal Commande_Avance { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Commande_DateCreation { get; set; }
        [ForeignKey("Statut_Livraison")]
        public int Commande_EtatDeLivraison { get; set; }
        [ForeignKey("User")]
        public string Commande_UtilisateurCommandeId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Commande_UtilisateurLivraisonId { get; set; } 
        [Column(TypeName = "nvarchar(150)")]
        public string Commande_NumeroTicket { get; set; }
        [ForeignKey("Position_Vente")]
        public int? Commande_CaissePayementId { get; set; }
        [Column(TypeName = "int")]
        public int? Commande_CaisseCommandeId { get; set; }
        [Column(TypeName = "int")]
        public int Commande_AbonnementId { get; set; }
        public ICollection<CommandeDetail> details { get; set; }
        public ICollection<Tva> Tva { get; set; }

        //public Point_Vente Point_Vente { get; set; }
        public Statut_Livraison Statut_Livraison { get; set; }
        public PositionVente Position_Vente { get; set; }
        public Statut_PaiementCommande Statut_PaiementCommande { get; set; }
        public ApplicationUser User { get; set; }
    }

}
