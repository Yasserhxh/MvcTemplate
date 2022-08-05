using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("FournisseurMatiere")]
    public class FournisseurMatiere
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Founisseur")]
        public int Founisseur_Id { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int MatierePremiere_Id { get; set; }
        public int Abonnement_ID { get; set; }
        public int IsActive { get; set; }
        public Fournisseur Founisseur { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
    }
}
