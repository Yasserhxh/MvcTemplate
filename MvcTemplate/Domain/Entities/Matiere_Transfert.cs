
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Matiere_Transfert")]
    public class Matiere_Transfert
    {
        [Key]
        public int MatiereTrans_ID { get; set; }
        [ForeignKey("MatierePremiere")]
        public int MatiereTrans_MatiereID { get; set; }
        public decimal MatiereTrans_Quantite { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int MatiereTrans_UniteID { get; set; }
        public string MatiereTrans_LotNumber { get; set; }
        [ForeignKey("Transfert_Matiere")]
        public int MatiereTrans_TransferID { get; set; }
        public MatierePremiere MatierePremiere { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Transfert_Matiere Transfert_Matiere { get; set; }
    }
}
