using System;

namespace Domain.Models
{
    public class ApprovisionnementModel
    {
        public int Approvisionnement_Id { get; set; }
        public DateTime Approvisionnement_Date { get; set; }
        public int Approvisionnement_PointVenteId { get; set; }
        public int Approvisionnement_ProduitId { get; set; }
        public int Approvisionnement_FormeProduitId { get; set; }
        public decimal Approvisionnement_Quantite { get; set; }
        public DateTime Approvisionnement_DateSaisie { get; set; }
        public string Approvisionnement_UtilisateurId { get; set; }
        public string Approvisionnement_ValideParId { get; set; }
        public int Approvisionnement_AtelierID { get; set; }
        public int Approvisionnement_AbonnementId { get; set; }
        public int Approvisionnement_Etat { get; set; }
        public int Approvisionnement_ProduitApproID { get; set; }
        public DateTime? Approvisionnement_DateModification { get; set; }
        public decimal Approvisionnement_QuantiteRestante { get; set; }
        public decimal Approvisionnement_CoutDeRevient { get; set; }

        public AtelierModel Atelier { get; set; }
        public ProduitVendableModel Produit_Vendable { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
        public ProduitApproModel ProduitAppro { get; set; }
        public Forme_ProduitModel Forme_Produit { get; set; }
    }

}
