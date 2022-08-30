using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("MatPrem_Allergene")]
    public class MatPrem_Allergene
    {
        [Key]
        public int MatPrem_AleergID { get; set; }
        [ForeignKey("MatierePremiere")]
        public int MatiereID { get; set; }
        [ForeignKey("Allgerene")]
        public int AllergenID { get; set; } 
        public int IsActive { get; set; } 
        public int AbonnementID { get; set; } 
        public MatierePremiere MatierePremiere { get; set; }    
        public Allergene Allgerene { get; set; }
    }
}
