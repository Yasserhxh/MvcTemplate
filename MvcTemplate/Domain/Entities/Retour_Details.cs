using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Retour_Details")]
    public class Retour_Details
    {
        [Key]
        public int RetourDetails_ID { get; set; }
        [ForeignKey("Retour_Produit")]
        public int RetourDetails_RetourID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int RetourDetails_FormeID { get; set; } 
        [ForeignKey("Unite_Mesure")]
        public int RetourDetails_UniteVente { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal RetourDetails_PrixProduit { get; set; }  
        [Column(TypeName ="decimal(18,2)")]
        public decimal RetourDetails_Quantite { get; set; } 
        [Column(TypeName ="decimal(18,2)")]
        public decimal RetourDetails_PrixTotal { get; set; }   
        [Column(TypeName ="smalldatetime")]
        public DateTime RetourDetails_DateRetour { get; set; }
        public int RetourDetails_isPerte { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public RetourProduits Retour_Produit { get; set; }
    }
}
