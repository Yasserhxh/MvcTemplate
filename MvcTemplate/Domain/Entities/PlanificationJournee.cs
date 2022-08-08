using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Planification_Journee")]
    public class PlanificationJournee
    {
        public PlanificationJournee()
        {
            Planification_Production = new Collection<PlanificationdeProduction>();
        }
        [Key]
        public int PlanificationJournee_ID { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Planification_GeneratedID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationJournee_Date { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string PlanificationJournee_Etat { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PlanificationJournee_CoutDeRevient { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PlanificationJournee_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int PlanificationJournee_AbonnementID { get; set; }
        [ForeignKey("BonDe_Sortie")]
        public int PlanificationJournee_BonDeSortieID { get; set; }
        [ForeignKey("Atelier")]
        public int? PlanificationJournee_AtelierID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int? PlanificationJournee_LieuStockageID { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string PlanificationJournee_UtilisateurId { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string PlanificationJournee_ValidePar { get; set; }
        [Column(TypeName = "bit")]
        public bool? PlanificationJournee_SeenByStock { get; set; }
        [Column(TypeName = "bit")]
        public bool? PlanificationJournee_SeenByAtelier { get; set; }
        public ICollection<PlanificationdeProduction> Planification_Production { get; set; }
        public BonDeSortie BonDe_Sortie { get; set; }
        public Atelier Atelier { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
    }
}
