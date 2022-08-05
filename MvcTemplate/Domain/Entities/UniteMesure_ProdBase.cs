using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("UniteMesure_ProdBase")]
    public class UniteMesure_ProdBase
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int UniteMesure_ID { get; set; }
        [ForeignKey("ProduitBase")]
        public int ProduitBase_ID { get; set; }
        public int isActive { get; set; }
        public int Abonnement_ID { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public ProduitBase ProduitBase { get; set; }
    }
}
