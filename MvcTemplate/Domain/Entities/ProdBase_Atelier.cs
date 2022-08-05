using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ProdBase_Atelier")]

    public class ProdBase_Atelier
    {
        [Key]
        public int ProdBase_Atelier_Id { get; set; }
        [ForeignKey("ProduitBase")]
        public int ProdBase_Atelier_ProduitID { get; set; }
        [Column(TypeName = "decimal(18,3)")]
        public decimal ProdBase_Atelier_QuantiteProduite { get; set; }
        [Column(TypeName = "int")]
        public int ProdBase_Atelier_AbonnementID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProdBase_Atelier_DateCreation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProdBase_Atelier_DateModification { get; set; }
        [ForeignKey("Atelier")]
        public int ProdBase_Atelier_AtelierID { get; set; }
        public Atelier Atelier { get; set; }
        public ProduitBase ProduitBase { get; set; }
    }
}
