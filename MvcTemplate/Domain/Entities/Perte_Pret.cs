using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    [Table("Perte_Pret")]

    public class Perte_Pret
    {    
        [Key]
        public int PertePret_ID { get; set; }
        [ForeignKey("ProduitPret_Atelier")]
        public int PertePret_ProduitPretAtelierID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PertePret_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int PertePret_UniteMesureID { get; set; }
        [ForeignKey("Atelier")]
        public int PertePret_AtelierID { get; set; }
        [Column(TypeName = "int")]
        public int PertePret_AbonnmentID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime PertePret_DateCreation { get; set; }
        public Atelier Atelier { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public ProduitPret_Atelier ProduitPret_Atelier { get; set; }
    }
}
