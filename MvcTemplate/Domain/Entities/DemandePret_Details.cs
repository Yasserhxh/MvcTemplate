using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("DemandePret_Details")]
    public class DemandePret_Details
    {
        [Key]
        public int DemandePretDetails_ID { get; set; }
        [ForeignKey("Demande_Pret")]
        public int DemandePretDetails_DemandeID { get; set; }
        [ForeignKey("Produit_PretConsomer")]
        public int DemandePretDetails_ProduitID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int DemandePretDetails_FormeID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int DemandePretDetails_UniteMesureID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DemandePretDetails_Quantite { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DemandePretDetails_QuantiteLivre { get; set; }
        public Demande_Pret Demande_Pret { get; set; }
        public ProduitPretConsomer Produit_PretConsomer { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
