using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Authentication;

namespace Domain.Entities
{
    [Table("Cloture_Journee")]
    public class ClotureJournee
    {
        [Key]
        public int ClotueJournee_ID { get; set; }
        [ForeignKey("Position_Vente")]
        public int ClotueJournee_PositionVenteID { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal ClotueJournee_SoldeDebit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClotueJournee_SoldeCredit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClotueJournee_Alimentation { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClotueJournee_Montant { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ClotueJournee_Description { get; set; }
        [ForeignKey("User")]
        public string ClotueJournee_UtilisateurID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ClotueJournee_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int ClotueJournee_AbonnementID { get; set; }
        [Column(TypeName = "int")]
        public int ClotueJournee_Valide { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ApplicationUser User { get; set; }

    }
}
