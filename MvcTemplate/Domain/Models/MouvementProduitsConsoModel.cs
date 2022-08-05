using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MouvementProduitsConsoModel
    {
        public int MouvementProduitsConso_Id { get; set; }
        public int MouvementProduitsConso_ProduitConsoId { get; set; }
        public DateTime MouvementProduitsConso_DateMouvement { get; set; }
        public int MouvementProduitsConso_MouvementType { get; set; }
        public decimal MouvementProduitsConso_Quantite { get; set; }
        public decimal MouvementProduitsConso_QuantiteActuelle { get; set; }
        public int MouvementProduitsConso_UniteMesureId { get; set; }
        public int MouvementProduitsConso_FournisseurProduitId { get; set; }
        public int? MouvementProduitsConso_StockFournisseurId { get; set; }
        public int? MouvementProduitsConso_ReceptionStockId { get; set; }
        public string MouvementProduitsConso_Utilisateur { get; set; }
        public bool MouvementProduitsConso_ReceptionStatut { get; set; }
        public string MouvementProduitsConso_ReceptionUtilisateur { get; set; }
        public int MouvementProduitsConso_AbonnementID { get; set; }
        public int MouvementProduitsConso_IsActive { get; set; }
        public ProduitConsomableStokageModel ProduitConsomable_Stokage { get; set; }
        public MouvementTypeModel Mouvement_Type { get; set; }
        public Unite_MesureModel Unite_Mesure { get; set; }
        public Lieu_StockageModel Lieu_Stockage { get; set; }
        public Point_VenteModel Point_Vente { get; set; }
    }
}
