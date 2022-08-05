using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Authentication;

namespace Domain.Entities
{
    [Table("Retrait_Caisse")]
    public class RetraitCaisse
    {
        [Key]
        public int RetraitCaisse_ID { get; set; }
        [ForeignKey("Retrait_Type")]
        public int RetraitCaisse_TypeRetraitID { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string RetraitCaisse_Motif { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal RetraitCaisse_Montant { get; set; }
        [Column(TypeName = "int")]
        public int RetraitCaisse_AbonnementID { get; set; }
        [ForeignKey("Position_Vente")]
        public int RetraitCaisse_PositionVenteID { get; set; }
        [ForeignKey("User")]
        public string RetraitCaisse_UtilisateurID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime RetraitCaisse_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int RetraitCaisse_FlagCloture { get; set; }
        public RetraitType Retrait_Type { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ApplicationUser User { get; set; }

    }
}
