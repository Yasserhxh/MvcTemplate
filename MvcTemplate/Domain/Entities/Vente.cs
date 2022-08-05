using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Vente")]
    public class Vente
    {
        public Vente()
        {
            Details = new Collection<VenteDetails>();
            Tva = new Collection<Tva>();
        }
        [Key]
        public int Vente_Id { get; set; }
        [ForeignKey("Point_Vente")]
        public int Vente_PointVenteId { get; set; } 
        [ForeignKey("Position_Vente")]
        public int Vente_PositionVenteId { get; set; }
        public DateTime Vente_Date { get; set; }
        public int? Vente_CommandeId { get; set; }
        public int? Vente_ProduitVendableId { get; set; }
        public decimal Vente_Quantite { get; set; }
        public decimal Vente_Prix { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Vente_TauxDeRemise { get; set; }
        public decimal Vente_PrixTotalRemise { get; set; }
        public int? Vente_PrixId { get; set; }
        public decimal Vente_Marge { get; set; }
        public int Vente_AbonnementId { get; set; }
        [ForeignKey("Mode_Paiement")]
        public int Vente_ModePaiement { get; set; }
        public string Vente_Commentaire { get; set; }
        public DateTime Vente_DateCreation { get; set; }
        public string Vente_UtilisateurId { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Vente_NumeroTicket { get; set; }
        public Point_Vente Point_Vente { get; set; }
        public ModePaiement Mode_Paiement { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ICollection<VenteDetails> Details { get; set; }
        public ICollection<Tva> Tva { get; set; }
    }
}
