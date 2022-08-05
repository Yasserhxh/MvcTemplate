using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("MatierePremiere_Stokage")]
    public class MatierePremiereStockage
    {
        [Key]
        public int MatierePremiereStokage_Id { get; set; }
        [ForeignKey("Section_Stockage")]
        public int MatierePremiereStokage_SectionStockageId { get; set; }
        [ForeignKey("Matiere_Premiere")]
        public int MatierePremiereStokage_MatierePremiereId { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal MatierePremiereStokage_StockMinimum { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal MatierePremiereStokage_StockMaximum { get; set; }
        public decimal MatierePremiereStokage_QuantiteActuelle { get; set; }
        public int MatierePremiereStokage_IsActive { get; set; }
        [Column(TypeName = "int")]
        public int MatierePremiereStokage_AbonnementId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime MatierePremiereStokage_DateCreation { get; set; }
        public Section_Stockage Section_Stockage { get; set; }
        public MatierePremiere Matiere_Premiere { get; set; }
    }
}
