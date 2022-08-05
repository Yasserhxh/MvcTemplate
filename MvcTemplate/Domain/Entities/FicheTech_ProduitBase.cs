using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("FicheTech_ProduitBase")]
    public class FicheTech_ProduitBase
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FicheTechnique_Bridge")]
        public int FicheTech_ID { get; set; }
        [ForeignKey("ProduitBase")]
        public int ProduitBase_ID { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public decimal ProduitQte { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int UniteMesure_ID { get; set; }
        public ProduitBase ProduitBase { get; set; }
        public FicheTechniqueBridge FicheTechnique_Bridge { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
