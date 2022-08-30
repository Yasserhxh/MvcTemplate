using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Allergene")]
    public class Allergene
    {       
        [Key]
        public int Allergene_Id { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Allergene_Libelle { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Allergene_LibelleAR { get; set; }
        public int Allergene_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int Allergene_AbonnementId { get; set; }
        public ICollection<MatPrem_Allergene> listMateires { get; set; }
       // public ICollection<MatierePremiere> MatierePremiere { get; set; }

    }

}
