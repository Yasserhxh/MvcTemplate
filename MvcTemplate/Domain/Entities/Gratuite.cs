using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Gratuite")]
    public class Gratuite
    {
        public Gratuite()
        {
            details = new Collection<GratuiteDetails>();
        }
        [Key]
        public int Gratuite_ID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Gratuite_CoutDeRevientTotal { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Gratuite_DateCreation { get; set; }
        [ForeignKey("Position_Vente")]
        public int Gratuite_PositionVente { get; set; }
        [Column(TypeName = "int")]
        public int Gratuite_AbonnementID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Gratuite_UtilisateurID { get; set; }
        public PositionVente Position_Vente { get; set; }
        public ICollection<GratuiteDetails> details { get; set; }

    }
}
