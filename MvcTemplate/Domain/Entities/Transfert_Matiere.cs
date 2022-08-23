using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Transfert_Matiere")]
    public class Transfert_Matiere
    {
        public Transfert_Matiere()
        {
            listeMatiere = new Collection<Matiere_Transfert>();
        }
        [Key]
        public int TransfertMat_ID { get; set; }
        public string TransfertMat_CreePar { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int TransfertMat_PointStockID { get; set; }
        public string TransfertMat_Statut { get; set; }
        public string TransfertMat_ValidePar { get; set; }
        public DateTime TransfertMat_DateCreation { get; set; }
        public DateTime? TransfertMat_DateValidation { get; set; }
        public int TransfertMat_AbonnementID { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public ICollection<Matiere_Transfert> listeMatiere { get; set; }
    }
}
