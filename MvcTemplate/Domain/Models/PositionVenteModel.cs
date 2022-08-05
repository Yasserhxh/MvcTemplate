using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PositionVenteModel
    {
        public PositionVenteModel()
        {
            Salles = new List<SalleModel>();
        }
        public int PositionVente_Id { get; set; }
        public int PositionVente_PointVenteId { get; set; }
        public int PositionVente_IsActive { get; set; }
        public string PositionVente_Libelle { get; set; }
        public int PositionVente_ModePaiementId { get; set; }
        public int PositionVente_AbonnementId { get; set; }
        public string PositionVente_UtilisateurId { get; set; }
        public DateTime PositionVente_DateCreation { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
        public ModePaiementModel Mode_Paiement { get; set; }
        public List<SalleModel> Salles { get; set; } 
    }
}
