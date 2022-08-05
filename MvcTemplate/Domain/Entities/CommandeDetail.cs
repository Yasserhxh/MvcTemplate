using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Commande_Detail")]
    public class CommandeDetail
    {
        [Key]
        public int CommandeDetail_Id { get; set; }
        [ForeignKey("Commande")]
        public int CommandeDetail_CommandeId { get; set; }
        [ForeignKey("Forme_Produit")]
        public int CommandeDetail_FormeProduitId { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal CommandeDetail_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int CommandeDetail_UniteId { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal CommandeDetail_Prix { get; set; }  
        [Column(TypeName = "int")]
        public int CommandeDetail_AbonnementId { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal CommandeDetail_CoutdeRevient { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal CommandeDetail_Marge { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Commande Commande { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }

    }

}
