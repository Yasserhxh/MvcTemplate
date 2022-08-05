using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("EchangeProduit_Details")]
    public class EchangeProduit_Details
    {
        [Key]
        public int EchangeProduitDetails_ID { get; set; }
        [ForeignKey("Echange_Produits")]
        public int EchangeProduitDetails_EchangeID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int EchangeProduitDetails_FromeID { get; set; }
        [Column(TypeName = "int")]
        public int EchangeProduitDetails_ProduitID { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal EchangeProduitDetails_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int EchangeProduitDetails_UniteID { get; set; }
        public Echange_Produits Echange_Produits { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
