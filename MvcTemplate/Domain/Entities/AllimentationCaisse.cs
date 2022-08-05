using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Authentication;

namespace Domain.Entities
{
    [Table("Allimentation_Caisse")]
    public class AllimentationCaisse
    {
        [Key]
        public int AllimentationCaisse_ID { get; set; }
        [ForeignKey("User")]
        public string AllimentationCaisse_UtilsateurID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AllimentationCaisse_Montant { get; set; }
        [Column(TypeName = "int")]
        public int AllimentationCaisse_PointVenteID { get; set; }
        [ForeignKey("Position_Vente")]
        public int AllimentationCaisse_PositionVenteID { get; set; }
        [Column(TypeName = "int")]
        public int AllimentationCaisse_AbonnementID { get; set; } 
        [Column(TypeName = "int")]
        public int AllimentationCaisse_FlagCloture { get; set; }
        public DateTime AllimentationCaisse_DateCreation { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ApplicationUser User { get; set; }

    }
}
