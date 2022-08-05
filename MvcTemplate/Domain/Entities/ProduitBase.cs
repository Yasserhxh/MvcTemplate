using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("ProduitBase")]
    public class ProduitBase
    {
        [Key]
        public int ProduitBase_ID { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string ProduitBase_Designation { get; set; }
        [ForeignKey("Forme_Stockage")]
        public int ProduitBase_FormeStockageID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int ProduitBase_UniteMesureID { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ProduitBase_CoutDeRevient { get; set; }
        [Column(TypeName = "int")]
        public int ProduitBase_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int ProduitBase_AbonnementID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ProduitBase_DateCreation { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Forme_Stockage Forme_Stockage { get; set; }
        public ICollection<UniteMesure_ProdBase> unites_Utilisation { get; set; }

    }
}
