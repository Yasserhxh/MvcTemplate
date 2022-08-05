using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Planification_ProdBase")]
    public class Planification_ProdBase
    {
        [Key]
        public int PlanificationProdBase_ID { get; set; }
        [ForeignKey("ProduitBase")]
        public int PlanificationProdBase_ProdBaseID { get; set; }
        [ForeignKey("Planification_Journee")]
        public int PlanificationProdBase_PlanificationID { get; set; }
        public string PlanificationProdBase_ProdBaseDesignation { get; set; }
        public decimal PlanificationProdBase_Quantite { get; set; }
        public string PlanificationProdBase_UniteDesi { get; set; }
        public ProduitBase ProduitBase { get; set; }
        public PlanificationJournee Planification_Journee { get; set; }
    }
}
