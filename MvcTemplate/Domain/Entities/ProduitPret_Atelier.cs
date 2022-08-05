using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ProduitPret_Atelier")]
    public class ProduitPret_Atelier
    {
        [Key]
        public int ProduitPretAtelier_ID { get; set; }
        [ForeignKey("Atelier")]
        public int ProduitPretAtelier_AtelierID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int ProduitPretAtelier_FormeID { get; set; }
        [ForeignKey("Produit_PretConsomer")]
        public int ProduitPretAtelier__ProduitID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProduitPretAtelier_Quantite { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProduitPretAtelier_DateCreation { get; set; }
        public Atelier Atelier { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public ProduitPretConsomer Produit_PretConsomer { get; set; }
    }
}
