using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Perte")]
    public class Perte
    {
        public Perte()
        {
            details = new Collection<PerteDetails>();
        }
        [Key]
        public int Perte_ID { get; set; }
        [Column(TypeName ="smalldatetime")]
        public DateTime Perte_DatePerte { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Perte_ValeurTotal { get; set; }
        [Column(TypeName = "int")]
        public int Perte_AbonnementID { get; set; }
        [ForeignKey("Position_Vente")]
        public int Perte_PositionVenteID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Perte_UtilisateurID { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Perte_DateCreation { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ICollection<PerteDetails> details { get; set; } 
    }
}
