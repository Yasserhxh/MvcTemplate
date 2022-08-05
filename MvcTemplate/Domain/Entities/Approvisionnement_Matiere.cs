using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Approvisionnement_Matiere")]
    public class Approvisionnement_Matiere
    {
        [Key]
        public int ApprovisionnementMatiere_ID { get; set; }
        [ForeignKey("MatierePremiereStockage")]
        public int ApprovisionnementMatiere_MatiereStockID { get; set; }
        [ForeignKey("Atelier")]
        public int ApprovisionnementMatiere_AtelierID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int ApprovisionnementMatiere_StockID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int ApprovisionnementMatiere_UniteID { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal ApprovisionnementMatiere_Quantite { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ApprovisionnementMatiere_DateCreation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ApprovisionnementMatiere_DateApprovisionnement { get; set; }
        public int ApprovisionnementMatiere_AbonnementID { get; set; }
        public string ApprovisionnementMatiere_Etat { get; set; }
        public string ApprovisionnementMatiere_Utilisateur { get; set; }
        public string ApprovisionnementMatiere_ValidéPar { get; set; }
        public MatierePremiereStockage MatierePremiereStockage { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
        public Atelier Atelier { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
    }
}
