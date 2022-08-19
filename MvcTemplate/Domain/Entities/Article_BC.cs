using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Article_BC")]

    public class Article_BC
    {
        [Key]
        public int ArticleBC_ID { get; set; }
        public string ArticleBC_Designation { get; set; }
        [ForeignKey("MatierePremiere")]
        public int ArticleBC_MatiereID { get; set; }
        public decimal ArticleBC_PU { get; set; }
        public decimal ArticleBC_Total { get; set; }
        public decimal ArticleBC_QteRest { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int ArticleBC_UniteMesure { get; set; }
        public decimal ArticleBC_Quantite { get; set; }
        [ForeignKey("bonDeCommande")]
        public int ArticleBC_BCID { get; set; }
        public BonDeCommande bonDeCommande { get; set; }
        public MatierePremiere MatierePremiere { get; set; }
        public Unite_Mesure Unite_Mesure { get; set; }
    }
}
