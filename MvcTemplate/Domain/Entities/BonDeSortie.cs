using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("BonDe_Sortie")]
    public class BonDeSortie
    {
        public BonDeSortie()
        {
            Bon_Details = new Collection<BonDetails>();

        }
        [Key]
        public int BonDeSortie_ID { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string BonDeSortie_Libelle { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime BonDeSortie_DateProduction { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime BonDeSortie_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int BonDeSortie_AbonnementID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int BonDeSortie_StockID { get; set; }

        public ICollection<BonDetails> Bon_Details { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; } 
        /*        public PlanificationJournee Planification_Journee { get; set; }
        */


    }
}
