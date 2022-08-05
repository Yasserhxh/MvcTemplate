using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Fiche_Forme")]
    public class FicheForme
    {
        [Key]
        public int FicheForme_ID { get; set; }
        [ForeignKey("Forme_Produit")]
        public int FicheForme_FormeProduit { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int FicheForme_uniteMesure { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FicheForme_CoutDeRevient { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FicheForme_Quantite { get; set; }
        [ForeignKey("FicheTechnique_Bridge")]
        public int FicheForme_FicheBridge { get; set; }
        [Column(TypeName = "int")]
        public int FicheForme_IsActive { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FicheForme_DateCreation { get; set; }
        public Forme_Produit Forme_Produit { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public FicheTechniqueBridge FicheTechnique_Bridge { get; set; }

    }
}
