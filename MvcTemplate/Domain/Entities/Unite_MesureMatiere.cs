using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Unite_MesureMatiere")]
    public class Unite_MesureMatiere
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int Unite_Id { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int MatierePremiere_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
    }
}
