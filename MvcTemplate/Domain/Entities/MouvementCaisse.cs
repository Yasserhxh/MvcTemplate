using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Mouvements_Caisse")]
    public class MouvementCaisse
    {
        [Key]
        public int MouvementsCaisse_ID { get; set; }
        [ForeignKey("MouvementCaisse_Type")]
        public int MouvementsCaisse_TypeID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MouvementsCaisse_Montant { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MouvementsCaisse_DateMouvement { get; set; }
        [ForeignKey("Position_Vente")]
        public int MouvementsCaisse_PositionVenteID { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string MouvementsCaisse_UtilisateurID { get; set; }
        [Column(TypeName = "int")]
        public int MouvementsCaisse_AbonnementID { get; set; }
        public MouvementCaisse_Type MouvementCaisse_Type { get; set; }
        public PositionVente Position_Vente { get; set; }
    }
}
