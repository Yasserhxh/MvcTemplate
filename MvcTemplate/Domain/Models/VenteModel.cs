using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class VenteModel
    {
        public VenteModel()
        {
            Details = new List<VenteDetailsModel>();
            Tva = new List<TvaModel>();
        }
        public int Vente_Id { get; set; }
        public int Vente_PointVenteId { get; set; }
        public int Vente_PositionVenteId { get; set; }
        public decimal Vente_TauxDeRemise { get; set; }
        public DateTime Vente_Date { get; set; }
        public int? Vente_CommandeId { get; set; }
        public decimal Vente_PrixTotalRemise { get; set; }
        public int? Vente_ProduitVendableId { get; set; }
        public decimal Vente_Quantite { get; set; }
        public int? Vente_PrixId { get; set; }
        public decimal Vente_Prix { get; set; }
        public decimal Vente_Marge { get; set; }
        public int Vente_AbonnementId { get; set; }
        public int Vente_ModePaiement { get; set; }
        public string Vente_Commentaire { get; set; }
        public string Vente_UtilisateurId { get; set; }
        public DateTime Vente_DateCreation { get; set; }
        public string Vente_NumeroTicket { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
        public ModePaiementModel Mode_Paiement { get; set; }
        public List<VenteDetailsModel> Details { get; set; }
        public List<TvaModel> Tva { get; set; }
        public PositionVenteModel Position_Vente { get; set; }
    }
}
