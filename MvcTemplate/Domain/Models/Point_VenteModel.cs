using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Point_VenteModel
    {
        public Point_VenteModel()
        {
            Famille_Link = new List<PointVente_FamilleModel>();
            PositionsVente = new List<PositionVenteModel>();
        }
        public int PointVente_Id { get; set; }
        public string PointVente_Nom { get; set; }
        public string PointVente_Adresse { get; set; }
        public string PointVente_NomResponsable { get; set; }
        public string PointVente_Telephone { get; set; }
        public int PointVente_IsActive { get; set; }
        public DateTime? PointVente_DateSaisie { get; set; }
        public string PointVente_UtilisateurId { get; set; }
        public int PointVente_AbonnementId { get; set; }
        public DateTime PointVente_DateCreation { get; set; }
        public int PointVente_VilleID { get; set; }
        public int? PointVente_AtelierID { get; set; }
        public int? PointVente_StockID { get; set; }
        public int PointVente_CodePostal { get; set; }
        public int PointVente_TypeService { get; set; }
        public VilleModel Ville { get; set; }
        public List<PointVente_FamilleModel> Famille_Link { get; set; }
        public List<PositionVenteModel> PositionsVente { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public AtelierModel Atelier { get; set; }
    }
}
