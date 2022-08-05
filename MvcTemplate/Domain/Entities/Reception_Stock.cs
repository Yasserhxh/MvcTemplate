using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Reception_Stock")]
    public class Reception_Stock
    {
        [Key]
        public int ReceptionStock_ID { get; set; }
        [ForeignKey("Atelier")]
        public int ReceptionStock_AtelierID { get; set; }
        [ForeignKey("Lieu_Stockage")]
        public int ReceptionStock_StockID { get; set; }
        [ForeignKey("MatierePremiere_Stokage")]
        public int ReceptionStock_MatiereID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int ReceptionStock_UniteID { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal ReceptionStock_Quantite { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ReceptionStock_DateReception { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime ReceptionStock_DateCreation { get; set; }
        [Column(TypeName = "int")]
        public int ReceptionStock_AbonnementID { get; set; }
        public Atelier Atelier { get; set; }
        public Lieu_Stockage Lieu_Stockage { get; set; }
        public MatierePremiereStockage MatierePremiere_Stokage { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
