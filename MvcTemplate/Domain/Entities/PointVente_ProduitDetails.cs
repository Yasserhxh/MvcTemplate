using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PointVente_ProduitDetails")]
    public class PointVente_ProduitDetails
    {
        [Key]
        public int PointVenteProduitDetails_ID { get; set; }
        [ForeignKey("PointVente_Stock")]
        public int PointVenteProduitDetails_PdvStockID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PointVenteProduitDetails_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PointVenteProduitDetails_PrixProduit { get; set; }
        public PointVente_Stock PointVente_Stock { get; set; }
    }
}
