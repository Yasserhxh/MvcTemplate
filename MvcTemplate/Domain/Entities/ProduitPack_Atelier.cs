using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
     [Table("ProduitPack_Atelier")]
    public class ProduitPack_Atelier
    {
        [Key]
        public int ProduitPackAtelier_ID { get; set; }
        [ForeignKey("Atelier")]
        public int ProduitPackAtelier_AtelierID { get; set; }
        [ForeignKey("ProduitPackage")]
        public int ProduitPackAtelier_ProduitPackID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int ProduitPackAtelier_FormeID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProduitPackAtelier_Quantite { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProduitPackAtelier_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int ProduitPackAtelier_AbonnementID { get; set; }
        public Atelier Atelier { get; set; }
        public ProduitPackage ProduitPackage { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
    }
}

