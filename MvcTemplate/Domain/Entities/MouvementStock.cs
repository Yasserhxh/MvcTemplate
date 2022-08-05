using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Mouvement_Stock")]
    public class MouvementStock
    {
        [Key]
        public int MouvementStock_Id { get; set; }
        [ForeignKey("MatierePremiere_Stokage")]
        public int MouvementStock_MatierePremiereStokageId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MouvementStock_Date { get; set; }
        [ForeignKey("Mouvement_Type")]
        public int MouvementStock_MouvementTypeId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MouvementStock_Quantite { get; set; } 
        [Column(TypeName = "decimal(18,2)")]
        public decimal MouvementStock_PrixAchatUnite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int MouvementStock_UniteMesureId { get; set; }
        [ForeignKey("Founisseur")]
        public int? MouvementStock_FournisseurId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MouvementStock_DateSaisie { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int? MouvementStock_DestinationStockId { get; set; }
        [Column(TypeName = "bit")]
        public bool MouvementStock_ReceptionStatutId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MouvementStock_DateReception { get; set; }
        [Column(TypeName = "int")]
        public int MouvementStock_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MouvementStock_DateCreation { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MouvementStock_MatiereQuantiteActuelle { get; set; }
        [Column(TypeName = "int")]
        public int MouvementStock_IsActive { get; set; }
        public MatierePremiereStockage MatierePremiere_Stokage { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Fournisseur Founisseur { get; set; }
        public MouvementType Mouvement_Type { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
    }
}
