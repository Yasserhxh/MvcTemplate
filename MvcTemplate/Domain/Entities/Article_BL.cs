using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("Article_BL")]
    public class Article_BL
    {
        [Key]
        public int ArticleBL_ID { get; set; }
        public string ArticleBL_Designation { get; set; }
        public string ArticleBL_LotFournisseur { get; set; }
        public string ArticleBL_LotTemp { get; set; }
        [ForeignKey("MatierePremiere")]
        public int ArticleBL_MatiereID { get; set; }
        [ForeignKey("Unite_Mesure")]
        public int ArticleBL_UniteMesureID { get; set; }
        public decimal ArticleBL_Quantie { get; set; }
        public decimal ArticleBL_PU { get; set; }
        public decimal ArticleBL_PrixTotal { get; set; }
        [ForeignKey("bonDeLivraison")]
        public int ArticleBL_BonLivraisonID { get; set; }
        public DateTime? ArticleBL_DateReception { get; set; }
        public DateTime? ArticleBL_DateProduction { get; set; }
        public DateTime? ArticleBL_DateLimiteConso { get; set; }
        public BonDeLivraison bonDeLivraison { get; set; }
        public MatierePremiere MatierePremiere { get; set;}
        public Unite_Mesure Unite_Mesure { get; set;}
    }
}
