using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Prix_Produit")]
    public class PrixProduit
    {
        [Key]
        public int PrixProduit_Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string PrixProduit_Description { get; set; }
        [ForeignKey("Forme_Produit")]
        public int PrixProduit_FormeProduitId { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrixProduit_Prix { get; set; }
        [Column(TypeName = "int")]
        public int? PrixProduit_TauxTVAId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrixProduit_QuantiteMinimale { get; set; }
        [Column(TypeName = "int")]
        public int PrixProduit_IsActive { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
    }
}
