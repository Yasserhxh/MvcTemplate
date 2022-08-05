using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("DetailsProduitAppro")]
    public class DetailsProduitAppro
    {
        [Key]
        public int DetailsProduitAppro_ID { get; set; }
        [ForeignKey("ProduitAppro")]
        public int DetailsProduitAppro_ProduitApproID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DetailsProduitAppro_QuantiteProduite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DetailsProduitAppro_PrixProduit { get; set; }
        public ProduitAppro ProduitAppro { get; set; }
    }
}
