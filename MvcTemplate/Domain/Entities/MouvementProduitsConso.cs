using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{ [Table("Mouvement_ProduitsConso")]
    public class MouvementProduitsConso
    {
        [Key]
        public int MouvementProduitsConso_Id { get; set; }
        [ForeignKey("ProduitConsomable_Stokage")]
        public int MouvementProduitsConso_ProduitConsoId { get; set; }
        public DateTime MouvementProduitsConso_DateMouvement { get; set; }
        [ForeignKey("Mouvement_Type")]
        public int MouvementProduitsConso_MouvementType { get; set; }
        public decimal MouvementProduitsConso_Quantite { get; set; }
        public decimal MouvementProduitsConso_QuantiteActuelle { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int MouvementProduitsConso_UniteMesureId { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int MouvementProduitsConso_FournisseurProduitId { get; set; }
        [ForeignKey("Point_Vente")]
        public int? MouvementProduitsConso_StockFournisseurId { get; set; }
        public int? MouvementProduitsConso_ReceptionStockId { get; set; }
        public string MouvementProduitsConso_Utilisateur { get; set; }
        public bool MouvementProduitsConso_ReceptionStatut { get; set; }
        public string MouvementProduitsConso_ReceptionUtilisateur { get; set; }
        public int MouvementProduitsConso_AbonnementID { get; set; }
        public int MouvementProduitsConso_IsActive { get; set; }
        public ProduitConsomableStokage ProduitConsomable_Stokage { get; set; }
        public MouvementType Mouvement_Type { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public Point_Vente Point_Vente { get; set; }
    }
}
