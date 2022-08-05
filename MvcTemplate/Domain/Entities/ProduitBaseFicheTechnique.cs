using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ProduitBaseFicheTechnique")]
    public class ProduitBaseFicheTechnique
    {
        [Key]
        public int ProduitBaseFicheTechnique_ID { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int ProduitBaseFicheTechnique_MatierePremiereID { get; set; }
        [ForeignKey("Unite_Mesure")] 
        public int ProduitBaseFicheTechnique_UniteMesureID { get; set; }
        [Column(TypeName = "decimal(8,2)")] 
        public decimal ProduitBaseFicheTechnique_QuantiteMatiere { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal ProduitBaseFicheTechnique_Prix { get; set; }
        [Column(TypeName = "int")] 
        public int ProduitBaseFicheTechnique_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")] 
        public DateTime ProduitBaseFicheTechnique_DateCreation { get; set; }
        [ForeignKey("FicheTechniqueProduitBase")]
        public int ProduitBaseFicheTechnique_FicheTehcniqueProduitBaseID { get; set; }
        
        public FicheTechniqueProduitBase FicheTechniqueProduitBase { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
    }
}
