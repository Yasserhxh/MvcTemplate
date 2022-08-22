using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Stock_Achat")]
    public class Stock_Achat
    {
        [Key]
        public int StockAchat_ID { get; set; }
        [ForeignKey("MatierePremiere")]
        public int StockAchat_MatiereID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int StockAchat_UniteMesureID { get; set; }
        public string StockAchat_LotIntern { get; set; }
        public string StockAchat_LotFournisseur { get; set; }
        public decimal StockAchat_QuantiteMatiere { get; set; }
        public decimal StockAchat_QuantiteRestante { get; set; }
        public int StockAchat_AbonnementID { get; set; }
        public MatierePremiere MatierePremiere { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
