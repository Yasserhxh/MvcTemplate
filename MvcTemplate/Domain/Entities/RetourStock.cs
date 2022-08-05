using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Retour_Stock")]
    public class RetourStock
    {
        [Key]
        public int RetourStok_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string RetourStok_Motif { get; set; }
        [Column(TypeName = "int")]
        public int RetourStok_Etat { get; set; }
        [ForeignKey("Planification_Journee")]
        public int RetourStok_PlanificationID { get; set; }
        [ForeignKey("BonDe_Sortie")]
        public int RetourStok_BonDeSortieID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime RetourStok_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int RetourStok_AbonnementID { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string RetourStok_UtilisateurID { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string RetourStok_ValideParID { get; set; }
        public PlanificationJournee Planification_Journee { get; set; }
        public BonDeSortie BonDe_Sortie { get; set; }

    }
}
