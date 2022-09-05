using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Matiere_Composants")]
    public class Matiere_Composants
    {
        [Key]
        public int MatiereComposants_ID { get; set; }
        [ForeignKey("MatierePremiere")]
        public int MatiereComposants_MatiereID { get; set; }
        public string MatiereComposants_Name { get; set; }
        public string MatiereComposants_NameAR { get; set; }
        public string MatiereComposants_Valeur { get; set; }
        public int MatiereComposants_IsActive { get; set; }
        public int MatiereComposants_AbonnementID { get; set; }
        public MatierePremiere MatierePremiere { get; set; }
    }
}
