using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Demande_Pret")]
    public class Demande_Pret
    {
        public Demande_Pret()
        {
            details = new Collection<DemandePret_Details>();
        }
        [Key]
        public int DemandePret_ID { get; set; }
        //[ForeignKey("Package_Production")]
        //public int DemandePret_ProductionID { get; set; }
        [ForeignKey("Atelier")]
        public int DemandePret_AtelierID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int DemandePret_StockID { get; set; }
        [Column(TypeName = "int")]
        public int DemandePret_AbonnementID { get; set; } 
        [Column(TypeName = "nvarchar(30)")]
        public string DemandePret_Etat { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime DemandePret_DateCreation { get; set; }
        //public PackageProduction Package_Production { get; set; }
        public Atelier Atelier { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public ICollection<DemandePret_Details> details { get; set; }
    }
}
