using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Demande")]
    public class Demande
    {
        [Key]
        public int Demande_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Demande_Type { get; set; }
        [ForeignKey("BonDe_Sortie")]
        public int? Demande_BonDeSortieID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int Demande_LieuStockageID { get; set; }
        [ForeignKey("Atelier")]
        public int Demande_AtelierID { get; set; }
        [ForeignKey("Planification_Journee")]
        public int? Demande_PlanificationID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Demande_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int Demande_AbonnementID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Demande_Etat { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Demande_Motif { get; set; }  
        [Column(TypeName = "nvarchar(450)")]
        public string Demande_UtilisateurID { get; set; } 
        [Column(TypeName = "nvarchar(450)")]
        public string Demande_ValideParID { get; set; }

        public BonDeSortie BonDe_Sortie { get; set; }
        public Atelier Atelier { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public PlanificationJournee Planification_Journee { get; set; }
    }
}
